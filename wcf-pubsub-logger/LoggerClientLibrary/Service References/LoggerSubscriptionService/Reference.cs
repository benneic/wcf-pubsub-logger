﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoggerClientLibrary.LoggerSubscriptionService {
    using System.Runtime.Serialization;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Logger.LogLevels", Namespace="http://schemas.datacontract.org/2004/07/LoggerLibrary.DataContracts")]
    public enum LoggerLogLevels : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Emergency = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Critical = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Error = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Important = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Informational = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Debug = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Trace = 7,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Logger.StatisticType", Namespace="http://schemas.datacontract.org/2004/07/LoggerLibrary.DataContracts")]
    public enum LoggerStatisticType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Integer = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Double = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        TimeSpan = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        String = 3,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="LoggerSubscriptionService.ILoggerSubscriptionService", CallbackContract=typeof(LoggerClientLibrary.LoggerSubscriptionService.ILoggerSubscriptionServiceCallback))]
    public interface ILoggerSubscriptionService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISubscriptionService/Subscribe", ReplyAction="http://tempuri.org/ISubscriptionService/SubscribeResponse")]
        void Subscribe(string eventOperation);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISubscriptionService/Unsubscribe", ReplyAction="http://tempuri.org/ISubscriptionService/UnsubscribeResponse")]
        void Unsubscribe(string eventOperation);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface ILoggerSubscriptionServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILoggerSubscriptionService/LoggingEvent")]
        void LoggingEvent(LoggerClientLibrary.LoggerSubscriptionService.LoggerLogLevels level, System.DateTime timeStamp, string processName, string subProcessName, string logCategory, string logMessage);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILoggerSubscriptionService/StatisticEvent")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(LoggerClientLibrary.LoggerSubscriptionService.LoggerLogLevels))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(LoggerClientLibrary.LoggerSubscriptionService.LoggerStatisticType))]
        void StatisticEvent(LoggerClientLibrary.LoggerSubscriptionService.LoggerStatisticType type, System.DateTime timeStamp, string processName, string subProcessName, string stasticCategory, string stasticName, object statisticValue);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILoggerSubscriptionService/CounterEvent")]
        void CounterEvent(string processName, string subProcessName, string counterCategory, string counterName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface ILoggerSubscriptionServiceChannel : LoggerClientLibrary.LoggerSubscriptionService.ILoggerSubscriptionService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class LoggerSubscriptionServiceClient : System.ServiceModel.DuplexClientBase<LoggerClientLibrary.LoggerSubscriptionService.ILoggerSubscriptionService>, LoggerClientLibrary.LoggerSubscriptionService.ILoggerSubscriptionService {
        
        public LoggerSubscriptionServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public LoggerSubscriptionServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public LoggerSubscriptionServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public LoggerSubscriptionServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public LoggerSubscriptionServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void Subscribe(string eventOperation) {
            base.Channel.Subscribe(eventOperation);
        }
        
        public void Unsubscribe(string eventOperation) {
            base.Channel.Unsubscribe(eventOperation);
        }
    }
}