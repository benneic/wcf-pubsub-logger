using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LoggerHostLibrary.DataContracts
{
    [DataContract]
    public class Event
    {
        [DataMember]
        public string ProcessName { get; set; }
        [DataMember]
        public string SubProcessName { get; set; }
        [DataMember]
        public DateTime DateTimeRouter { get; set; }
    }
}
