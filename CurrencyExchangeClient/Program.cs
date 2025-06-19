using System;
using System.Threading.Tasks;
using System.ServiceModel;

namespace CurrencyExchangeClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // Create a channel to communicate with our service
                var factory = new ChannelFactory<ICurrencyExchangeService>(
                    new BasicHttpBinding(),
                    new EndpointAddress("http://localhost:8733/CurrencyExchangeService")
                );

                var service = factory.CreateChannel();

                // Test getting current exchange rate
                Console.WriteLine("Getting current USD exchange rate...");
                var usdRate = await service.GetCurrentExchangeRate("USD");
                Console.WriteLine(string.Format("Current USD rate: {0}", usdRate));

                // Test creating an account
                Console.WriteLine("\nCreating a test account...");
                var accountCreated = await service.CreateAccount("testuser", "password123");
                Console.WriteLine(string.Format("Account created: {0}", accountCreated));

                // Test adding funds
                Console.WriteLine("\nAdding funds to account...");
                var fundsAdded = await service.AddFunds("testuser", 1000, "PLN");
                Console.WriteLine(string.Format("Funds added: {0}", fundsAdded));

                // Test getting balance
                Console.WriteLine("\nChecking account balance...");
                var balance = await service.GetBalance("testuser", "PLN");
                Console.WriteLine(string.Format("Current balance: {0} PLN", balance));

                // Test buying currency
                Console.WriteLine("\nBuying USD...");
                var bought = await service.BuyCurrency("testuser", "PLN", "USD", 100);
                Console.WriteLine(string.Format("Currency bought: {0}", bought));

                // Test getting historical rate
                Console.WriteLine("\nGetting historical USD rate...");
                var historicalRate = await service.GetHistoricalRate("USD", DateTime.Now.AddDays(-1));
                Console.WriteLine(string.Format("Yesterday's USD rate: {0}", historicalRate));

                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error: {0}", ex.Message));
                Console.ReadKey();
            }
        }
    }
} 