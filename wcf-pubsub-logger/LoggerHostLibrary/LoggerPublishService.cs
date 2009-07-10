using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ServiceModelEx;
using LoggerHostLibrary.ServiceContracts;
using LoggerHostLibrary.DataContracts;

namespace LoggerHostLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class LoggerPublishService : PublishService<ILogger> , ILogger
    {
        #region ILogger Members

        public void Log(LoggingEvent logEvent)
        {
            logEvent.DateTimeRouter = DateTime.Now;
            FireEvent(logEvent);
        }

        public void Statistic(StatisticEvent statEvent)
        {
            statEvent.DateTimeRouter = DateTime.Now;
            FireEvent(statEvent);
        }

        public void Counter(CounterEvent counterEvent)
        {
            counterEvent.DateTimeRouter = DateTime.Now;
            FireEvent(counterEvent);
        }

        #endregion
    }
}
