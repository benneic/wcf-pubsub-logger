// © 2009 AlgoBox Ltd. All rights reserved
// Benn Eichhorn

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoggerClientLibrary.LoggerPublishService;
using System.Threading;
using LoggerClientLibrary.Collections.Generics;

namespace LoggerClientLibrary
{
    public class Logger
    {
        static object m_ObjLock = new object();
        static Logger m_Logger = null;

        static public Logger Log
        {
            get
            {
                if (m_Logger == null)
                {
                    lock (m_ObjLock)
                    {
                        if (m_Logger == null)
                        {
                            m_Logger = new Logger();
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
            if(m_LoggerClient == null)
            {
                m_Events = new AutoResetEvent[2];
                m_Events[THREAD_CONTINUE] = m_EventContinue;
                m_Events[THREAD_EXIT] = m_EventExit;

                try
                {
                    lock (m_LoggerClient)
                    {
                        m_LoggerClient = new LoggerClient();

                        m_Thread = new Thread(new ThreadStart(ProcessThread));
                        m_Thread.Name = "PubSubLoggerClient";
                        m_Thread.Priority = ThreadPriority.BelowNormal;
                        m_Thread.Start();
                    }
                    return true;
                }
                catch
                {
                    m_LoggerClient = null;
                }
            }
            return false;
        }

        protected bool Close()
        {
            if (m_LoggerClient != null)
            {
                lock (m_LoggerClient)
                {
                    m_EventExit.Set();
                    ProcessQueue();

                    if (m_Thread != null)
                    {
                        m_Thread.Abort();
                        m_Thread = null;
                    }
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
                if (signalValue == THREAD_CONTINUE)
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
                    m_LoggerClient.LoggingEvent((LoggingEvent)loggerEvent);
                }
                else if (loggerEvent is StatisticEvent)
                {
                    m_LoggerClient.StatisticEvent((StatisticEvent)loggerEvent);
                } 
                else if (loggerEvent is CounterEvent)
                {
                    m_LoggerClient.CounterEvent((CounterEvent)loggerEvent);
                }
            }
        }

        

        public void Log(
    }
}
