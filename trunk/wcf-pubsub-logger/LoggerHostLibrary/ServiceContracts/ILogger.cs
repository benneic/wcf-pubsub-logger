using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceModelEx;
using LoggerHostLibrary.DataContracts;

namespace LoggerHostLibrary.ServiceContracts
{
    [ServiceContract(Namespace = "http://algobox.net/PubSubLogger/2009/07")]
    public interface ILogger
    {
        [OperationContract(IsOneWay = true)]
        void Log(LoggingEvent logEvent);

        [OperationContract(IsOneWay = true)]
        void Statistic(StatisticEvent statEvent);

        [OperationContract(IsOneWay = true)]
        void Counter(CounterEvent counterEvent);
    }   
}
