using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using LoggerClientLibrary;

namespace LoggerClientSubscriptionConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Program TestPublisher = new Program();
            TestPublisher.RunConsole();
        }

        private void RunConsole()
        {
            LogSubscriber.Logger.LogEvent += new LogSubscriber.EventHandlerLogging(Logger_LogEvent);
            LogSubscriber.Logger.CounterEvent += new LogSubscriber.EventHandlerCounter(Logger_CounterEvent);
            LogSubscriber.Logger.StatisticEvent += new LogSubscriber.EventHandlerStatistics(Logger_StatisticEvent);

            bool isActive = true;
            while (isActive)
            {
                Console.Write("Enter X to exit...");
                string proceed = Console.ReadLine().ToUpper().Trim();
                if (proceed == "X")
                {
                    isActive = false;
                }
            }
        }


        void Logger_StatisticEvent(LoggerClientLibrary.LoggerSubscriptionService.StatisticEvent e)
        {
            if(e.StatisticType == LoggerClientLibrary.LoggerSubscriptionService.StatisticEvent.StatisticTypes.Integer)
                Console.WriteLine("Stat\t=> {0}, {1}, {2} - {3} {4}", e.ProcessName, e.SubProcessName, e.StatisticCategory, e.StatisticValueInt, e.StatisticName);
        }

        void Logger_CounterEvent(LoggerClientLibrary.LoggerSubscriptionService.CounterEvent e)
        {
            Console.WriteLine("Count\t=> {0}, {1}, {2}, {3}", e.ProcessName, e.SubProcessName, e.CounterCategory, e.CounterName);
        }

        void Logger_LogEvent(LoggerClientLibrary.LoggerSubscriptionService.LoggingEvent e)
        {
            //Console.WriteLine("Log\t=> {0}, {1}, {2}, {3}, {4}", e.ProcessName, e.SubProcessName, e.LogCategory, e.LogLevel, e.LogMessage);
        }

        
    }
}
