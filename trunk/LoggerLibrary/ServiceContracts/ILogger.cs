using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceModelEx;

namespace LoggerLibrary.ServiceContracts
{
    public interface ILogger
    {
        [OperationContract(IsOneWay = true)]
        bool LoggingEvent(int instrumentId, DateTime dateStart, DateTime dateEnd);

        [OperationContract(IsOneWay = true)]
        bool StatisticEvent(int instrumentId, DateTime dateStart, DateTime dateEnd);

        [OperationContract(IsOneWay = true)]
        bool CounterEvent(int instrumentId, DateTime dateStart, DateTime dateEnd);
    }

    [ServiceContract(CallbackContract = typeof(ILogger))]
    public interface ILoggerService : ISubscriptionService
    { }
}
