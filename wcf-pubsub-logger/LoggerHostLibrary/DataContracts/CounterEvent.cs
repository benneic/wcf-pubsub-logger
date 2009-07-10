using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LoggerHostLibrary.DataContracts
{
    [DataContract]
    public class CounterEvent : Event
    {
        [DataMember]
        public string CounterCategory { get; set; }
        [DataMember]
        public string CounterName { get; set; }

    }
}
