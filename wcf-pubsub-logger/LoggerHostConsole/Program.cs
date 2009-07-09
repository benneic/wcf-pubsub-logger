using System;
using System.ServiceModel;
using LoggerLibrary;
using LoggerLibrary.Helpers;
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

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();

            publishing.Close();
            subscribing.Close();

        }
    }
}
