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
        void LoggingEvent(Logger.LogLevels level, DateTime timeStamp, string processName, string moduleName, string methodName, string details);

        [OperationContract(IsOneWay = true)]
        void StatisticEvent(Logger.StatisticType type, DateTime timeStamp, string processName, string moduleName, string stasticName, object statisticValue);

        [OperationContract(IsOneWay = true)]
        void CounterEvent(string processName, string counterGroupName, string counterName);
    }   
}
