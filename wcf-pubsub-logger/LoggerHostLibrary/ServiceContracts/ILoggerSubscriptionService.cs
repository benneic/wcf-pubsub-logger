﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ServiceModelEx;

namespace LoggerHostLibrary.ServiceContracts
{
    [ServiceContract(CallbackContract = typeof(ILogger),Namespace = "http://algobox.net/PubSubLogger/2009/07")]
    public interface ILoggerSubscriptionService : ISubscriptionService
    { }
}
