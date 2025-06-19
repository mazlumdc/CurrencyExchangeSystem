using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CurrencyExchangeService
{
    // Service contract that defines the operations our currency exchange service will provide
    [ServiceContract]
    public interface ICurrencyExchangeService
    {
        // Get current exchange rate for a specific currency
        [OperationContract]
        Task<decimal> GetCurrentExchangeRate(string currencyCode);

        // Create a new user account
        [OperationContract]
        bool CreateAccount(string username, string password);

        // Add money to user's account
        [OperationContract]
        bool AddFunds(string username, string currency, double amount);

        // Get user's account balance
        [OperationContract]
        Dictionary<string, double> GetBalances(string username);

        // Buy currency
        [OperationContract]
        bool BuyCurrency(string username, string fromCurrency, string toCurrency, double amount);

        // Sell currency
        [OperationContract]
        bool SellCurrency(string username, string fromCurrency, string toCurrency, double amount);

        // Get historical exchange rates
        [OperationContract]
        Task<decimal> GetHistoricalRate(string currencyCode, DateTime date);

        [OperationContract]
        bool Login(string username, string password);

        [OperationContract]
        Task<ExchangeRates> GetExchangeRates();

        [OperationContract]
        double GetExchangeRate(string fromCurrency, string toCurrency);
    }

    [DataContract]
    public class ExchangeRates
    {
        [DataMember]
        public double USD { get; set; }

        [DataMember]
        public double EUR { get; set; }

        [DataMember]
        public double GBP { get; set; }

        [DataMember]
        public double PLN { get; set; }
    }
} 