using System;
using System.ServiceModel;
using CurrencyExchangeService;

namespace CurrencyExchangeHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(CurrencyExchangeService.CurrencyExchangeService)))
            {
                host.Open();
                Console.WriteLine("Currency Exchange Service is running...");
                Console.WriteLine("Press any key to stop the service.");
                Console.ReadKey();
                host.Close();
            }
        }
    }
} 