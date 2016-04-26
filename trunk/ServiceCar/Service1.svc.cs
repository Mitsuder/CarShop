using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.OleDb;

namespace ServiceCar
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IService1
    {
        public Car[] load()
        {
            List<Car> a = new List<Car>();
            Car b;
            Dictionary<int, string> TranceType = new Dictionary<int, string>();
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

            com = new OleDbCommand("SELECT * FROM Car", connection);
            try
            {
                connection.Open();
                OleDbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    b = new Car();
                    b.manufacturer = reader.GetString(0);
                    b.model = reader.GetString(1);
                    b.dat = reader.GetDateTime(2);
                    b.volume = reader.GetDouble(3);
                    b.power = reader.GetInt32(4);
                    b.trancemission = TranceType[reader.GetInt32(5)];
                    a.Add(b);
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            finally { connection.Close(); }

            return a.ToArray();

        }

        public bool save(Car [] c)
        {
            Dictionary<int, string> TranceType = new Dictionary<int, string>();

            bool success = true;
            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='C://Users/Фирсов/Documents/Visual Studio 2015/Projects/CarShop/trunk/db1.mdb'");
            OleDbCommand command = new OleDbCommand("SELECT * FROM Trancemission", connection);
            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TranceType.Add(reader.GetInt32(0), reader.GetString(1));
                }
            }
            catch (Exception ex) { throw new Exception("", ex); }
            finally { connection.Close(); }

            command = new OleDbCommand("DELETE FROM Car", connection);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch(Exception) { success = false; }
            finally { connection.Close();}

            string s = null;
            for (int i = 0; i < c.Length; i++)
            {
                int key = TranceType.FirstOrDefault(x => x.Value == c[i].trancemission).Key;
                s = string.Format("INSERT INTO Car (manufacturer, model, dat,volume,power,trancemissionID) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')",c[i].manufacturer, c[i].model, c[i].dat.ToShortDateString(), c[i].volume, c[i].power, key);
                command = new OleDbCommand(s, connection);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception) { success = false; }
                finally { connection.Close(); }

            }
            return success;
        }
    }
}
