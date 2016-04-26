using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CarShopAccessDB
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<Car> GetList();
        [OperationContract]
        List<Car> Add(string s, Car[] car);
        [OperationContract]
        List<Car> Remove(List<Car> l, int iD);
        [OperationContract]
        List<Car> Edit(List<Car> l, string s);
        [OperationContract]
        void Save(List<Car> l);
        [OperationContract]
        List<Car> Sort(List<Car> l);
    }


    [DataContract]
    public class Car : IComparable
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

        public int CompareTo(object c)
        {
            Car obj = (Car)c;
            if (this == null || obj == null)
                throw new ArgumentException("объект не объект");

            else
                switch (this.manufacturer.CompareTo(obj.manufacturer))
                {
                    case 1: return 1;
                    default: return -1;
                    case 0:
                        switch (this.model.CompareTo(obj.model))
                        {
                            case 1: return 1;
                            case 0: return 0;
                            default: return -1;
                        }
                }

        }
        
        public override string ToString()
        {
            string result = String.Format("ID={6} {0} , {1} , {2}, v={3:N1}, p={4}, Trancemission is {5}", manufacturer, model, dat.ToLongDateString(), volume, power, trancemission, id);
            return result;
        }


        public Car(string man, string mod, DateTime data, double vol, int pow, string tra, int ID)
        {
            manufacturer = man;
            model = mod;
            dat = data;
            volume = vol;
            power = pow;
            id = ID;
            trancemission = tra;
        }
    }

}
