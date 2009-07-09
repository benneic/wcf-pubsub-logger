using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceModelEx;
using LoggerLibrary.DataContracts;

namespace LoggerLibrary.ServiceContracts
{
    [ServiceContract]
    public interface ILogger
    {
        [OperationContract(IsOneWay = true)]
        void LoggingEvent(LoggerLibrary.DataContracts.Logger.LogLevels level, DateTime timeStamp, string processName, string subProcessName, string logCategory, string logMessage);

        [OperationContract(IsOneWay = true)]
        void StatisticEvent(LoggerLibrary.DataContracts.Logger.StatisticType type, DateTime timeStamp, string processName, string subProcessName, string stasticCategory, string stasticName, object statisticValue);

        [OperationContract(IsOneWay = true)]
        void CounterEvent(string processName, string subProcessName, string counterCategory, string counterName);
    }   
}
