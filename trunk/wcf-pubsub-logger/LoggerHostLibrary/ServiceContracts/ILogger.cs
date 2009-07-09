using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceModelEx;
using LoggerHostLibrary.DataContracts;

namespace LoggerHostLibrary.ServiceContracts
{
    [ServiceContract]
    public interface ILogger
    {
        [OperationContract(IsOneWay = true)]
        void LoggingEvent(LoggingEvent logEvent);

        [OperationContract(IsOneWay = true)]
        void StatisticEvent(StatisticEvent statEvent);

        [OperationContract(IsOneWay = true)]
        void CounterEvent(CounterEvent countEvent);
    }   
}
