﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarForm.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/load", ReplyAction="http://tempuri.org/IService1/loadResponse")]
        ServiceCar.Car[] load();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/load", ReplyAction="http://tempuri.org/IService1/loadResponse")]
        System.Threading.Tasks.Task<ServiceCar.Car[]> loadAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/save", ReplyAction="http://tempuri.org/IService1/saveResponse")]
        bool save(ServiceCar.Car[] c);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/save", ReplyAction="http://tempuri.org/IService1/saveResponse")]
        System.Threading.Tasks.Task<bool> saveAsync(ServiceCar.Car[] c);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : CarForm.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<CarForm.ServiceReference1.IService1>, CarForm.ServiceReference1.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ServiceCar.Car[] load() {
            return base.Channel.load();
        }
        
        public System.Threading.Tasks.Task<ServiceCar.Car[]> loadAsync() {
            return base.Channel.loadAsync();
        }
        
        public bool save(ServiceCar.Car[] c) {
            return base.Channel.save(c);
        }
        
        public System.Threading.Tasks.Task<bool> saveAsync(ServiceCar.Car[] c) {
            return base.Channel.saveAsync(c);
        }
    }
}