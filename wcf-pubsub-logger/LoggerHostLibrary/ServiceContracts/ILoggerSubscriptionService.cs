using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ServiceModelEx;

namespace LoggerLibrary.ServiceContracts
{
    [ServiceContract(CallbackContract = typeof(ILogger))]
    public interface ILoggerSubscriptionService : ISubscriptionService
    { }
}
