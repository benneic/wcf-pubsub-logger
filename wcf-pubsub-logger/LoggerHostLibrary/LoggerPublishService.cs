using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ServiceModelEx;
using LoggerHostLibrary.ServiceContracts;
using LoggerHostLibrary.DataContracts;
using System.Threading;

namespace LoggerHostLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class LoggerPublishService : PublishService<ILogger> , ILogger
    {
        static TimerCallback m_TimerDelegate1Min = new TimerCallback(Process1MinCounters);
        static TimerCallback m_TimerDelegate1Second = new TimerCallback(Process1Second);
        static Timer m_Timer1Min = new System.Threading.Timer(m_TimerDelegate1Min, null, new TimeSpan(0, 1, 0), new TimeSpan(0, 1, 0));
        static Timer m_Timer1Second = new System.Threading.Timer(m_TimerDelegate1Second, null, new TimeSpan(0, 0, 1), new TimeSpan(0, 0, 1));
        static int m_CountMessagesTotal = 0;
        static int m_CountMessagesLast1Min = 0;
        static int m_CountMessagesLast1Sec = 0;

        #region ILogger Members

        public void Log(LoggingEvent logEvent)
        {
            logEvent.DateTimeRouter = DateTime.Now;
            FireEvent(logEvent);
            Interlocked.Increment(ref m_CountMessagesTotal);
        }

        public void Statistic(StatisticEvent statEvent)
        {
            statEvent.DateTimeRouter = DateTime.Now;
            FireEvent(statEvent);
            Interlocked.Increment(ref m_CountMessagesTotal);
        }

        public void Counter(CounterEvent counterEvent)
        {
            counterEvent.DateTimeRouter = DateTime.Now;
            FireEvent(counterEvent);
            Interlocked.Increment(ref m_CountMessagesTotal);
        }

        #endregion


        static private void Process1MinCounters(Object unused)
        {
            StatisticEvent statEvent = new StatisticEvent();
            statEvent.StatisticType = StatisticEvent.StatisticTypes.Integer;
            statEvent.StatisticValueInt = m_CountMessagesTotal - m_CountMessagesLast1Min;
            statEvent.DateTimeSource = DateTime.Now;
            statEvent.ProcessName = "PubSubLogger";
            statEvent.SubProcessName = "Statistics";
            statEvent.StatisticCategory = "Message Counters";
            statEvent.StatisticName = "Messages per minute";
            FireEvent("Statistic", statEvent);
            Interlocked.Exchange(ref m_CountMessagesLast1Min, m_CountMessagesTotal);
        }


        static private void Process1Second(Object unused)
        {
            StatisticEvent statEvent = new StatisticEvent();
            statEvent.StatisticType = StatisticEvent.StatisticTypes.Integer;
            statEvent.StatisticValueInt = m_CountMessagesTotal - m_CountMessagesLast1Sec;
            statEvent.DateTimeSource = DateTime.Now;
            statEvent.ProcessName = "PubSubLogger";
            statEvent.SubProcessName = "Statistics";
            statEvent.StatisticCategory = "Message Counters";
            statEvent.StatisticName = "Messages per second";
            FireEvent("Statistic", statEvent);
            Interlocked.Exchange(ref m_CountMessagesLast1Sec, m_CountMessagesTotal);
        }
    }
}
