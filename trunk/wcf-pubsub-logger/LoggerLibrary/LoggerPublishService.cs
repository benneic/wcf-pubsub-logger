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
    class LoggerPublishService : PublishService<ILogger> , ILogger
    {

        #region ILogger Members

        public void LoggingEvent(LoggerLibrary.DataContracts.Logger.LogLevels level, DateTime timeStamp, string processName, string moduleName, string methodName, string details)
        {
            FireEvent(level, timeStamp, processName, moduleName, methodName, details);
        }

        public void StatisticEvent(LoggerLibrary.DataContracts.Logger.StatisticType type, DateTime timeStamp, string processName, string moduleName, string stasticName, object statisticValue)
        {
            FireEvent(type, timeStamp, processName, moduleName, stasticName, statisticValue);
        }

        public void CounterEvent(string processName, string counterGroupName, string counterName)
        {
            FireEvent(processName, counterGroupName, counterName);
        }

        #endregion
    }
}
