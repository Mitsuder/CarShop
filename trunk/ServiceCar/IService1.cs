using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiceCar
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        Car[] load();
        [OperationContract]
        bool save(Car[] c);
    }


    // Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
    [DataContract]
    public class Car
    {
        [DataMember]
        public string manufacturer;
        [DataMember]
        public string model;
        [DataMember]
        public DateTime dat;
        [DataMember]
        public double volume;
        [DataMember]
        public int power;
        [DataMember]
        public int id;
        [DataMember]
        public string trancemission;
    }
}
