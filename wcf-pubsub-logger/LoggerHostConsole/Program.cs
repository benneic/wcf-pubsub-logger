using System;
using System.ServiceModel;
using LoggerHostLibrary;
using LoggerHostLibrary.ServiceContracts;
using ServiceModelEx;

namespace LoggerHostConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost subscribing = new ServiceHost(typeof(LoggerSubscriptionService));
            ServiceHost publishing = new ServiceHost(typeof(LoggerPublishService));           
            
            subscribing.Open();
            publishing.Open();

            LoggerSubscriptionService tester = new LoggerSubscriptionService();

            Console.WriteLine("Press the Enter key to quit...");
            Console.ReadLine();

            publishing.Close();
            subscribing.Close();

        }
    }
}
