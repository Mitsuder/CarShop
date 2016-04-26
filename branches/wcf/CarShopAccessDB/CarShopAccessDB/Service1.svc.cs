using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.OleDb;

namespace CarShopAccessDB
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IService1
    {
        Dictionary<int, string> TranceType = new Dictionary<int, string>();

        public List<Car> Add(string s, Car[] car)
        {
            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='C://Users/Фирсов/Documents/Visual Studio 2015/Projects/CarShop/trunk/db1.mdb'");
            OleDbCommand com = new OleDbCommand("SELECT * FROM Trancemission", connection);
            try
            {
                connection.Open();
                OleDbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    TranceType.Add(reader.GetInt32(0), reader.GetString(1));
                }
            }
            catch (Exception ex) { throw new Exception("", ex); }
            finally { connection.Close(); }

            int i = -1;
            List<Car> c = new List<Car>();
            string[] st = s.Split(';');
            foreach (KeyValuePair<int, string> O in TranceType)
            {
                if (O.Value == st[5])
                    i = O.Key;
            }
            if (i > 0)
            {
                c = car.ToList();
                c.Add(new Car(st[0], st[1], DateTime.Parse(st[2]), double.Parse(st[3]), Int32.Parse(st[4]), st[5], (car[car.Length - 1].id + 1)));

                string commanda = String.Format("INSERT INTO Car (manufacturer, model, dat, volume, power,TrancemissionID) VALUES ('{0}','{1}','{2}',{3},{4},{5})", st[0], st[1], DateTime.Parse(st[2]), double.Parse(st[3]), Int32.Parse(st[4]), i);
                com = new OleDbCommand(commanda, connection);
                try
                {
                    connection.Open();
                    com.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception("", ex); }
                finally { connection.Close(); }
            }
                return c;
        }

        public List<Car> Edit(List<Car> l, string s)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetList()
        {
            Car c;
            List<Car> spis = new List<Car>(); 
            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='C://Users/Фирсов/Documents/Visual Studio 2015/Projects/CarShop/trunk/db1.mdb'");
            OleDbCommand com = new OleDbCommand("SELECT * FROM Trancemission", connection);
            try
            {
                connection.Open();
                OleDbDataReader reader =  com.ExecuteReader();
                while (reader.Read())
                {
                    TranceType.Add(reader.GetInt32(0), reader.GetString(1));
                }
            }
            catch(Exception ex) { throw new Exception("", ex); }
            finally { connection.Close(); }

            com = new OleDbCommand("SELECT * FROM Car", connection);
            try
            {
                connection.Open();
                OleDbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    c = new Car(reader.GetString(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDouble(3), reader.GetInt32(4), TranceType[reader.GetInt32(5)], reader.GetInt32(6));
                    spis.Add(c);
                }
            }
            catch(Exception) {}
            finally { connection.Close(); }

            return spis;
        }

        public List<Car> Remove(List<Car> l, int iD)
        {
            throw new NotImplementedException();
        }

        public void Save(List<Car> l)
        {
            throw new NotImplementedException();
        }

        public List<Car> Sort(List<Car> l)
        {
            throw new NotImplementedException();
        }
    }
}
