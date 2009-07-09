using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LoggerLibrary.DataContracts
{
    [DataContract]
    public class Logger
    {
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

        [DataContract]
        public enum StatisticType
        {
            [EnumMember]
            Integer,
            [EnumMember]
            Double,
            [EnumMember]
            TimeSpan,
            [EnumMember]
            String
        }
    }
}
