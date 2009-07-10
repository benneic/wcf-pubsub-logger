﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using LoggerClientLibrary;

namespace LoggerClientPublisherConsole
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
            LoggerClientLibrary.LogPublisher log = LoggerClientLibrary.LogPublisher.Logger;

            Console.TreatControlCAsInput = true;
            bool isActive = true;            
            while (isActive)
            {
                LoggerClientLibrary.LoggerPublishService.LoggingEvent.LogLevels level = LoggerClientLibrary.LoggerPublishService.LoggingEvent.LogLevels.Important;
                Console.Write("Enter ProcessName: ");
                string processName = Console.ReadLine();
                Console.Write("Enter SubProcessName: ");
                string subProcessName = Console.ReadLine();
                Console.Write("Enter LogCategory: ");
                string logCategory = Console.ReadLine();
                Console.Write("Enter LogMessage: ");
                string logMessage = Console.ReadLine();
                Console.Write("Submit:\n{0}\n{1}\n{2}\n{3}\n? (y/n)", processName, subProcessName, logCategory, logMessage);
                string proceed = Console.ReadLine().ToUpper().Trim();
                if (proceed == "Y")
                {
                    log.Log(level, processName, subProcessName, logCategory, logMessage);
                }
                
            }
        }
    }
}
