using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ServiceModelEx;
using LoggerLibrary.ServiceContracts;

namespace LoggerLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class LoggerPublishService : PublishService<ILogger> , ILogger
    {
        #region ILogger Members

        public void LoggingEvent(LoggerLibrary.DataContracts.Logger.LogLevels level, DateTime timeStamp, string processName, string subProcessName, string logCategory, string logMessage)
        {
            FireEvent(level, DateTime.Now, processName, subProcessName, logCategory, logMessage);
        }

        public void StatisticEvent(LoggerLibrary.DataContracts.Logger.StatisticType type, DateTime timeStamp, string processName, string subProcessName, string stasticCategory, string stasticName, object statisticValue)
        {
            FireEvent(type, DateTime.Now, processName, subProcessName, stasticCategory, stasticName, statisticValue);
        }

        public void CounterEvent(string processName, string subProcessName, string counterCategory, string counterName)
        {
            FireEvent(processName, subProcessName, counterCategory, counterName);
        }

        #endregion
    }
}
