using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LoggerHostLibrary.DataContracts
{
    [DataContract]
    public class StatisticEvent : Event
    {
        [DataMember]
        public StatisticTypes StatisticType { get; set; }
        [DataMember]
        public DateTime DateTimeSource { get; set; }
        [DataMember]
        public string StatisticCategory { get; set; }
        [DataMember]
        public string StatisticName { get; set; }
        [DataMember]
        public int StatisticValueInt { get; set; }
        [DataMember]
        public double StatisticValueDouble { get; set; }
        [DataMember]
        public TimeSpan StatisticValueTimeSpan { get; set; }
        [DataMember]
        public string StatisticValueString { get; set; }

        [DataContract]
        public enum StatisticTypes
        {
            [EnumMember]
            None,
            [EnumMember]
            Integer,
            [EnumMember]
            Double,
            [EnumMember]
            TimeSpan,
            [EnumMember]
            String,
            [EnumMember]
            IntegerTimeSpan,
            [EnumMember]
            DoubleTimeSpan
        }
    }
}
