using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ServiceModelEx;
using LoggerHostLibrary.ServiceContracts;

namespace LoggerHostLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class LoggerSubscriptionService : SubscriptionManager<ILogger>, ILoggerSubscriptionService
    {
    }
}
