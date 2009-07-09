using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ServiceModelEx;
using LoggerLibrary.ServiceContracts;

namespace LoggerLibrary
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    public class LoggerSubscriptionService : SubscriptionManager<ILogger>, ILoggerSubscriptionService
    {
    }
}
