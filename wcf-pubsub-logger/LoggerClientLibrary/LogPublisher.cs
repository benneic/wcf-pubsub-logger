// © 2009 AlgoBox Ltd. All rights reserved
// Benn Eichhorn

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ServiceModel.Channels;
using LoggerClientLibrary.Collections.Generics;
using LoggerClientLibrary.LoggerPublishService;

namespace LoggerClientLibrary
{
    public class LogPublisher
    {
        static object m_ObjLock = new object();
        static LogPublisher m_Logger = null;

        static public LogPublisher Logger
        {
            get
            {
                if (m_Logger == null)
                {
                    lock (m_ObjLock)
                    {
                        if (m_Logger == null)
                        {
                            m_Logger = new LogPublisher();
                            m_Logger.Open();
                        }
                    }
                }
                return m_Logger;
            }
        }


        LoggerClient m_LoggerClient = null;

        AutoResetEvent[] m_Events = null;
        AutoResetEvent m_EventExit = new AutoResetEvent(false);
        AutoResetEvent m_EventContinue = new AutoResetEvent(false);

        const Int16 THREAD_CONTINUE = 0;
        const Int16 THREAD_EXIT = 1;

        Thread m_Thread = null;

        LockFreeQueue<Event> m_Queue = new LockFreeQueue<Event>();

        protected bool Open()
        {
            if (m_LoggerClient == null)
            {
                m_Events = new AutoResetEvent[2];
                m_Events[THREAD_CONTINUE] = m_EventContinue;
                m_Events[THREAD_EXIT] = m_EventExit;

                // Allow exception to be thrown
                try
                {
                    lock (m_ObjLock)
                    {
                        m_LoggerClient = new LoggerClient();

                        m_LoggerClient.Open();

                        m_Thread = new Thread(new ThreadStart(ProcessThread));
                        m_Thread.Name = "PubSubLoggerClient";
                        m_Thread.Priority = ThreadPriority.BelowNormal;
                        m_Thread.Start();
                    }
                    return true;
                }
                finally
                {
                    System.Diagnostics.Debug.Assert(m_LoggerClient.State == System.ServiceModel.CommunicationState.Opened, "Could not connect to PubSubHost");
                    m_LoggerClient = null;
                }
            }
            return false;
        }

        protected bool Close()
        {
            if (m_LoggerClient != null)
            {
                lock (m_ObjLock)
                {
                    m_EventExit.Set();
                    ProcessQueue();

                    if (m_Thread != null)
                    {
                        m_Thread.Abort();
                        m_Thread = null;
                    }
                    if (m_LoggerClient.State == System.ServiceModel.CommunicationState.Opened)
                    {
                        m_LoggerClient.Close();
                    }
                    m_LoggerClient = null;
                }
            }

            return false;
        }

        protected void ProcessThread()
        {
            bool isAlive = true;
            int signalValue;

            while (isAlive)
            {
                signalValue = WaitHandle.WaitAny(m_Events);

                if (m_LoggerClient.State != System.ServiceModel.CommunicationState.Opened)
                {
                    isAlive = false;
                    lock (m_ObjLock)
                    {
                        m_LoggerClient = null;
                    }
                }
                else if (signalValue == THREAD_CONTINUE)
                {
                    ProcessQueue();
                }
                else if (signalValue == THREAD_EXIT)
                {
                    isAlive = false;
                }
            }
        }


        private void ProcessQueue()
        {
            Event loggerEvent = null;
            while (m_Queue.Deque(ref loggerEvent))
            {
                if (loggerEvent is LoggingEvent)
                {
                    m_LoggerClient.Log((LoggingEvent)loggerEvent);
                }
                else if (loggerEvent is StatisticEvent)
                {
                    m_LoggerClient.Statistic((StatisticEvent)loggerEvent);
                }
                else if (loggerEvent is CounterEvent)
                {
                    m_LoggerClient.Counter((CounterEvent)loggerEvent);
                }
            }
        }



        public void Log(LoggingEvent.LogLevels logLevel, string processName, string subProcessName, string logCategory, string logMessage)
        {
            LoggingEvent logEvent = new LoggingEvent();
            logEvent.LogLevel = logLevel;
            logEvent.DateTimeSource = DateTime.Now;
            logEvent.ProcessName = processName;
            logEvent.SubProcessName = subProcessName;
            logEvent.LogCategory = logCategory;
            logEvent.LogMessage = logMessage;
            m_Queue.Enqueue(logEvent);
            m_EventContinue.Set();
        }

        public void Count(string processName, string subProcessName, string counterCategory, string counterName)
        {
            CounterEvent counterEvent = new CounterEvent();
            counterEvent.ProcessName = processName;
            counterEvent.SubProcessName = subProcessName;
            counterEvent.CounterCategory = counterCategory;
            counterEvent.CounterName = counterName;
            m_Queue.Enqueue(counterEvent);
            m_EventContinue.Set();
        }



        public void Stat(string processName, string subProcessName, string statCategory, string statName, int statValue)
        {
            StatisticEvent statEvent = new StatisticEvent();
            statEvent.StatisticType = StatisticEvent.StatisticTypes.Integer;
            statEvent.StatisticValueInt = statValue;
            statEvent.DateTimeSource = DateTime.Now;
            statEvent.ProcessName = processName;
            statEvent.SubProcessName = subProcessName;
            statEvent.StatisticCategory = statCategory;
            statEvent.StatisticName = statName;
            m_Queue.Enqueue(statEvent);
            m_EventContinue.Set();
        }

        public void Stat(string processName, string subProcessName, string statCategory, string statName, double statValue)
        {
            StatisticEvent statEvent = new StatisticEvent();
            statEvent.StatisticType = StatisticEvent.StatisticTypes.Double;
            statEvent.StatisticValueDouble = statValue;
            statEvent.DateTimeSource = DateTime.Now;
            statEvent.ProcessName = processName;
            statEvent.SubProcessName = subProcessName;
            statEvent.StatisticCategory = statCategory;
            statEvent.StatisticName = statName;
            m_Queue.Enqueue(statEvent);
            m_EventContinue.Set();
        }

        public void Stat(string processName, string subProcessName, string statCategory, string statName, string statValue)
        {
            StatisticEvent statEvent = new StatisticEvent();
            statEvent.StatisticType = StatisticEvent.StatisticTypes.String;
            statEvent.StatisticValueString = statValue;
            statEvent.DateTimeSource = DateTime.Now;
            statEvent.ProcessName = processName;
            statEvent.SubProcessName = subProcessName;
            statEvent.StatisticCategory = statCategory;
            statEvent.StatisticName = statName;
            m_Queue.Enqueue(statEvent);
            m_EventContinue.Set();
        }

        public void Stat(string processName, string subProcessName, string statCategory, string statName, TimeSpan statValue)
        {
            StatisticEvent statEvent = new StatisticEvent();
            statEvent.StatisticType = StatisticEvent.StatisticTypes.TimeSpan;
            statEvent.StatisticValueTimeSpan = TimeSpan.Zero;
            statEvent.DateTimeSource = DateTime.Now;
            statEvent.ProcessName = processName;
            statEvent.SubProcessName = subProcessName;
            statEvent.StatisticCategory = statCategory;
            statEvent.StatisticName = statName;
            m_Queue.Enqueue(statEvent);
            m_EventContinue.Set();
        }

        public void Stat(string processName, string subProcessName, string statCategory, string statName, double statValue, TimeSpan timeSpan)
        {
            StatisticEvent statEvent = new StatisticEvent();
            statEvent.StatisticType = StatisticEvent.StatisticTypes.DoubleTimeSpan;
            statEvent.StatisticValueTimeSpan = timeSpan;
            statEvent.StatisticValueDouble = statValue;
            statEvent.DateTimeSource = DateTime.Now;
            statEvent.ProcessName = processName;
            statEvent.SubProcessName = subProcessName;
            statEvent.StatisticCategory = statCategory;
            statEvent.StatisticName = statName;
            m_Queue.Enqueue(statEvent);
            m_EventContinue.Set();
        }

        public void Stat(string processName, string subProcessName, string statCategory, string statName, int statValue, TimeSpan timeSpan)
        {
            StatisticEvent statEvent = new StatisticEvent();
            statEvent.StatisticType = StatisticEvent.StatisticTypes.IntegerTimeSpan;
            statEvent.StatisticValueTimeSpan = timeSpan;
            statEvent.StatisticValueInt = statValue;
            statEvent.DateTimeSource = DateTime.Now;
            statEvent.ProcessName = processName;
            statEvent.SubProcessName = subProcessName;
            statEvent.StatisticCategory = statCategory;
            statEvent.StatisticName = statName;
            m_Queue.Enqueue(statEvent);
            m_EventContinue.Set();
        }

    }

}
