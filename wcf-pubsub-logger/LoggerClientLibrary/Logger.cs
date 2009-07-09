using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoggerClientLibrary.LoggerPublishService;
using System.Threading;

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

        Thread m_Thread = null;

        protected bool Open()
        {
            if(m_LoggerClient == null)
            {
                m_Events = new AutoResetEvent[2];
                m_Events[0] = m_EventContinue;
                m_Events[1] = m_EventExit;

                try
                {
                    lock (m_LoggerClient)
                    {
                        m_LoggerClient = new LoggerClient();

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
            return false;
        }

        protected void ProcessQueue()
        {

        }
    }
}
