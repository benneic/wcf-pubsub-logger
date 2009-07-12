using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using LoggerClientLibrary.LoggerSubscriptionService;


namespace LoggerClientLibrary
{
    public class LogSubscriber : ILoggerSubscriptionServiceCallback
    {
        static object m_ObjLock = new object();
        static LogSubscriber m_Logger = null;
        

        static public LogSubscriber Logger
        {
            get
            {
                if (m_Logger == null)
                {
                    lock (m_ObjLock)
                    {
                        if (m_Logger == null)
                        {
                            m_Logger = new LogSubscriber();
                            
                            m_Logger.Open();
                        }
                    }
                }
                return m_Logger;
            }
        }


        LoggerSubscriptionServiceClient m_LoggerClient = null;

        InstanceContext m_LoggerCallbackInstance = null;

        public delegate void EventHandlerLogging(LoggingEvent e);
        public delegate void EventHandlerStatistics(StatisticEvent e);
        public delegate void EventHandlerCounter(CounterEvent e);

        public event EventHandlerLogging LogEvent;
        public event EventHandlerStatistics StatisticEvent;
        public event EventHandlerCounter CounterEvent;

        protected bool Open()
        {
            if (m_LoggerClient == null)
            {
                lock (m_ObjLock)
                {
                    m_LoggerCallbackInstance = new InstanceContext(m_Logger);
                    m_LoggerClient = new LoggerSubscriptionServiceClient(m_LoggerCallbackInstance);
                    m_LoggerClient.Open();
                    m_LoggerClient.Subscribe(String.Empty);
                }
                return true; 
            }
            return false;
        }

        protected bool Close()
        {
            if (m_LoggerClient != null)
            {
                if (m_LoggerClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    m_LoggerClient.Close();
                }
                m_LoggerClient = null;
            }
            return false;
        }


        #region ILoggerSubscriptionServiceCallback Members

        public void Log(LoggingEvent logEvent)
        {
            LogEvent(logEvent);
        }

        public void Statistic(StatisticEvent statEvent)
        {
            StatisticEvent(statEvent);
        }

        public void Counter(CounterEvent counterEvent)
        {
            CounterEvent(counterEvent);
        }

        #endregion
    }
}
