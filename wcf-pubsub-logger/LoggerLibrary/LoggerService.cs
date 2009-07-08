using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceModelEx;
using LoggerLibrary.ServiceContracts;

namespace LoggerLibrary
{
    class LoggerService : SubscriptionManager<ILogger>, ILoggerService
    {
        


    }
}
