using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LoggerHostLibrary.DataContracts
{
    [DataContract]
    public class LoggingEvent : IEvent
    {
        public LoggingEvent(LogLevels logLevel, DateTime timeStamp, string processName, string subProcessName, string logCategory, string logMessage)
        {
            LogLevel = logLevel;
            DateTimeSource = timeStamp;
            ProcessName = processName;
            SubProcessName = subProcessName;
            LogCategory = logCategory;
            LogMessage = logMessage;
        }

        [DataMember]
        public LogLevels LogLevel { get; set; }
        [DataMember]
        public DateTime DateTimeSource { get; set; }
        [DataMember]
        public DateTime DateTimeRouter { get; set; }
        [DataMember]
        public string ProcessName { get; set; }
        [DataMember]
        public string SubProcessName { get; set; }
        [DataMember]
        public string LogCategory { get; set; }
        [DataMember]
        public string LogMessage { get; set; }

        [DataContract]
        public enum LogLevels
        {
            [EnumMember]
            Emergency = 1,
            [EnumMember]
            Critical = 2,
            [EnumMember]
            Error = 3,
            [EnumMember]
            Important = 4,
            [EnumMember]
            Informational = 5,
            [EnumMember]
            Debug = 6,
            [EnumMember]
            Trace = 7
        }

    }
}
