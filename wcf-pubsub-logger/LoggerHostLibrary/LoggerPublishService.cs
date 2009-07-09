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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class LoggerPublishService : PublishService<ILogger> , ILogger
    {
        #region ILogger Members

        public void LoggingEvent(LoggingEvent logEvent)
        {
            logEvent.DateTimeRouter = DateTime.Now;
            FireEvent(logEvent);
        }

        public void StatisticEvent(StatisticEvent statEvent)
        {
            statEvent.DateTimeRouter = DateTime.Now;
            FireEvent(statEvent);
        }

        public void CounterEvent(CounterEvent countEvent)
        {
            countEvent.DateTimeRouter = DateTime.Now;
            FireEvent(countEvent);
        }

        #endregion
    }
}
