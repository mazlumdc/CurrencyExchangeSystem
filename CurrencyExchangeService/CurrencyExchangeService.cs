using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Net.Http;
using System.Runtime.Serialization;

namespace CurrencyExchangeService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CurrencyExchangeService : ICurrencyExchangeService
    {
        private Dictionary<string, UserAccount> _accounts = new Dictionary<string, UserAccount>();
        private Dictionary<string, decimal> _exchangeRates = new Dictionary<string, decimal>
        {
            { "USD", 1.0m },
            { "EUR", 0.85m },
            { "GBP", 0.73m },
            { "PLN", 3.8m }
        };

        private readonly HttpClient _httpClient;
        private const string NBP_API_BASE_URL = "http://api.nbp.pl/api/exchangerates/rates/a/";

        public CurrencyExchangeService()
        {
            _httpClient = new HttpClient();
        }

        public bool CreateAccount(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            if (_accounts.ContainsKey(username))
                return false;

            _accounts[username] = new UserAccount
            {
                Username = username,
                Password = password,
                Balances = new Dictionary<string, double>
                {
                    { "USD", 1000 },
                    { "EUR", 1000 },
                    { "GBP", 1000 },
                    { "PLN", 1000 }
                }
            };

            return true;
        }

        public bool Login(string username, string password)
        {
            if (!_accounts.ContainsKey(username))
                return false;

            return _accounts[username].Password == password;
        }

        public Dictionary<string, double> GetBalances(string username)
        {
            if (!_accounts.ContainsKey(username))
                throw new FaultException(string.Format("Account not found: {0}", username));

            return _accounts[username].Balances;
        }

        public bool AddFunds(string username, string currency, double amount)
        {
            if (!_accounts.ContainsKey(username))
                throw new FaultException(string.Format("Account not found: {0}", username));

            if (!_accounts[username].Balances.ContainsKey(currency))
                throw new FaultException(string.Format("Currency not supported: {0}", currency));

            _accounts[username].Balances[currency] += amount;
            return true;
        }

        public bool BuyCurrency(string username, string fromCurrency, string toCurrency, double amount)
        {
            if (!_accounts.ContainsKey(username))
                throw new FaultException(string.Format("Account not found: {0}", username));

            if (!_accounts[username].Balances.ContainsKey(fromCurrency))
                throw new FaultException(string.Format("Currency not supported: {0}", fromCurrency));

            if (!_accounts[username].Balances.ContainsKey(toCurrency))
                throw new FaultException(string.Format("Currency not supported: {0}", toCurrency));

            if (!_exchangeRates.ContainsKey(fromCurrency) || !_exchangeRates.ContainsKey(toCurrency))
                throw new FaultException("Invalid currency pair");

            var fromBalance = _accounts[username].Balances[fromCurrency];
            if (fromBalance < amount)
                throw new FaultException(string.Format("You don't have enough {0}", fromCurrency));

            var exchangeRate = (double)(_exchangeRates[toCurrency] / _exchangeRates[fromCurrency]);
            var convertedAmount = amount * exchangeRate;

            _accounts[username].Balances[fromCurrency] -= amount;
            _accounts[username].Balances[toCurrency] += convertedAmount;

            return true;
        }

        public bool SellCurrency(string username, string fromCurrency, string toCurrency, double amount)
        {
            if (!_accounts.ContainsKey(username))
                throw new FaultException(string.Format("Account not found: {0}", username));

            if (!_accounts[username].Balances.ContainsKey(fromCurrency))
                throw new FaultException(string.Format("Currency not supported: {0}", fromCurrency));

            if (!_accounts[username].Balances.ContainsKey(toCurrency))
                throw new FaultException(string.Format("Currency not supported: {0}", toCurrency));

            if (!_exchangeRates.ContainsKey(fromCurrency) || !_exchangeRates.ContainsKey(toCurrency))
                throw new FaultException("Invalid currency pair");

            var fromBalance = _accounts[username].Balances[fromCurrency];
            if (fromBalance < amount)
                throw new FaultException(string.Format("Insufficient balance in {0}", fromCurrency));

            var exchangeRate = (double)(_exchangeRates[fromCurrency] / _exchangeRates[toCurrency]);
            var convertedAmount = amount * exchangeRate;

            _accounts[username].Balances[fromCurrency] -= amount;
            _accounts[username].Balances[toCurrency] += convertedAmount;

            return true;
        }

        public double GetExchangeRate(string fromCurrency, string toCurrency)
        {
            if (!_exchangeRates.ContainsKey(fromCurrency) || !_exchangeRates.ContainsKey(toCurrency))
                throw new FaultException("Invalid currency pair");

            return (double)(_exchangeRates[toCurrency] / _exchangeRates[fromCurrency]);
        }

        public async Task<decimal> GetCurrentExchangeRate(string currencyCode)
        {
            if (!_exchangeRates.ContainsKey(currencyCode))
                throw new FaultException(string.Format("Currency not supported: {0}", currencyCode));

            return _exchangeRates[currencyCode];
        }

        public async Task<ExchangeRates> GetExchangeRates()
        {
            return new ExchangeRates
            {
                USD = (double)_exchangeRates["USD"],
                EUR = (double)_exchangeRates["EUR"],
                GBP = (double)_exchangeRates["GBP"],
                PLN = (double)_exchangeRates["PLN"]
            };
        }

        // Get historical exchange rate from NBP API
        public async Task<decimal> GetHistoricalRate(string currencyCode, DateTime date)
        {
            try
            {
                // Format date for NBP API
                var dateStr = date.ToString("yyyy-MM-dd");
                
                // Call NBP API to get historical rate
                var response = await _httpClient.GetStringAsync(string.Format("{0}{1}/{2}", NBP_API_BASE_URL, currencyCode.ToLower(), dateStr));
                var xml = XDocument.Parse(response);
                
                // Extract the rate from XML response
                var rate = xml.Descendants("Rate")
                             .Select(x => decimal.Parse(x.Element("Mid").Value))
                             .FirstOrDefault();

                return rate;
            }
            catch (Exception)
            {
                // In case of error, return 0
                return 0;
            }
        }
    }

    public class UserAccount
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Dictionary<string, double> Balances { get; set; }
    }
} 