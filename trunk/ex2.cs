using System;
using System.IO;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Configuration;

class Car : IComparable
{
    public enum trancemission { AT, MT };
    private string man;
    private string mod;
    private DateTime d;
    private double vol;
    private int pow;
    private trancemission tra;
    private int id;




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
                case 0: switch (this.model.CompareTo(obj.model))
                    {
                        case 1: return 1;
                        case 0: return 0;
                        default: return -1;
                    }
            }
    }


    public string manufacturer
    {
        get {return man;}
    }

    public string model
    {
        get {return mod;}
    }

    public DateTime date
    {
        get {return d;}
    }

    public double volume
    {
        get {return vol;}
    }

    public int power
    {
        get {return pow;}
    }

    public trancemission trance
    {
        get { return tra; }
    }

    public int ID
    {
        get { return id; }
    }

    public Car(string s)                          //конструктор
    {
        string[] sub = s.Split(';');
        if (sub.Length == 7)
        {
            id = Action.Id++;
            man = sub[0];
            mod = sub[1];
            d = DateTime.Parse(sub[2]);
            vol = Double.Parse(sub[3]);
            pow = Int32.Parse(sub[4]);
            if (sub[5] == "AT") tra = trancemission.AT;
            else tra = trancemission.MT;
            return;
        }
        throw new Exception("Обнаружена некорректная строка. Объект не будет создан");
    }

    public Car(string s, int i)                          //конструктор
    {
        string[] sub = s.Split(';');
        if (sub.Length == 7)
        {
            id = i;
            man = sub[0];
            mod = sub[1];
            d = DateTime.Parse(sub[2]);
            vol = Double.Parse(sub[3]);
            pow = Int32.Parse(sub[4]);
            if (sub[5] == "AT") tra = trancemission.AT;
            else tra = trancemission.MT;
            return;
        }
        throw new Exception(@"[обнаружена некорректная строка. Объект не будет создан]");
    }

    public override string ToString()
    {
        string result =String.Format("ID={6} {0} , {1} , {2}, v={3:N1}, p={4}, Trancemission is {5}",manufacturer,model,date.ToLongDateString(),volume,power,trance,ID);
        return result;
    }

}

class baseWork
{
    public static List<Car> OpenBase()
    {
        Action.obj.Clear();
        Action.Id = 0;
        OleDbConnection con = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);

        Dictionary<int, string> x = new Dictionary<int, string>();
        OleDbCommand command = new OleDbCommand("SELECT * FROM Trancemission", con);
        try
        {
            con.Open();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                x.Add(reader.GetInt32(0), reader.GetString(1));
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message.ToString()); }
        finally { con.Close(); }

        command = new OleDbCommand("SELECT * FROM Car", con);
        try
        {
            con.Open();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string str = reader.GetString(0) + ";" + reader.GetString(1) + ";" + reader.GetDateTime(2).ToShortDateString() + ";" + reader.GetDouble(3) + ";" + reader.GetInt32(4) + ";" + x[reader.GetInt32(5)] + ";";
                Action.obj.Add(new Car(str, reader.GetInt32(6)));
            }
            Console.WriteLine("Информация из БД успешно загружена");
        }
        catch (Exception ex) { Console.WriteLine(ex.Message.ToString()); }
        finally { con.Close(); }
        return Action.obj;
    }

    public static void AddItem(string s)
    {
        string[] temp = s.Split(';');
        int i = -1;
        foreach (KeyValuePair<int, string> O in Action.TranceDic)
        {
            if (O.Value == temp[5])
                i = O.Key;
        }
        if (i > 0)
        {
            OleDbConnection con = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
            string comm = String.Format("INSERT INTO Car (manufacturer, model, dat, volume, power,TrancemissionID) VALUES ('{0}','{1}','{2}',{3},{4},{5})", temp[0], temp[1], DateTime.Parse(temp[2]).ToShortDateString(), temp[3], temp[4],i);
            OleDbCommand command = new OleDbCommand(comm, con);
            try
            {
                con.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message.ToString()); }
            finally { con.Close(); }
            Action.obj.Add(new Car(s));
            Console.WriteLine("объект добавлен");
        }
        else Console.WriteLine("нет такой коробки");
        return;
    }
    

    public static void RemoveItem(int x)
    {
        Action.idx = x;
        Action.idx = Action.obj.FindIndex(Action.IsID);
        if (Action.idx >= 0)
        {
                OleDbConnection con = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
            string comm = "DELETE FROM Car WHERE id = " + x;
            OleDbCommand command = new OleDbCommand(comm, con);
            try
            {
                con.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message.ToString()); }
            finally { con.Close(); }
            Action.obj.RemoveAt(Action.idx);
            Console.WriteLine("объект с ID={0} удален", x);
            Action.idx = -1;
            return;
        }
        Console.WriteLine("не удалось найти объект с ID={0}", x);
        return;
    }

    public static void EditItem(int x, string s)
    {
        string[] temp = s.Split(';');

        int i = -1;
        foreach (KeyValuePair<int, string> O in Action.TranceDic)
        {
            if (O.Value == temp[5])
                i = O.Key;
        }
        if (i >= 0)
        {
            OleDbConnection con = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
            string com = String.Format("UPDATE Car SET manufacturer='{0}', model='{1}', dat='{2}', volume = '{3}', power ='{4}', TrancemissionID='{5}' WHERE ID ={6}", temp[0], temp[1], DateTime.Parse(temp[2]).ToShortDateString(), temp[3], temp[4],i, x);

            Console.WriteLine(com);
            OleDbCommand command = new OleDbCommand(com, con);
            try
            {
                con.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message.ToString());}
            finally { con.Close(); }

        Action.idx = x;
        Action.idx = Action.obj.FindIndex(Action.IsID);
        if (Action.idx >= 0)
        {
            Action.obj[Action.idx] = new Car(s);
            Console.WriteLine("объект с ID={0} заменен", x);
            Action.idx = -1;
            return;
        }
        }
        Console.WriteLine("не удалось найти объект с ID={0}", x);
    }
}

class textWork
{
    public static List<Car> OpenFile(string s)
    {
        Action.obj.Clear();
        Action.Id = 0;
        StreamReader file = null;
        try
        {
            file = new StreamReader(s);
            Console.WriteLine("Файл {0} открыт", s);
            string temp = file.ReadLine();
            while (temp != null)
            {
                Action.obj.Add(new Car(temp));
                temp = file.ReadLine();

            }
        }
        catch (Exception exc)
        {
            Console.WriteLine("не удалось открыть файл {0} потому что {1}", s, exc.Message);
        }
        finally { file.Close(); }
        return Action.obj;
    }

    public static void SaveFile(List<Car> obj, string s)
    {
        StreamWriter file = null;
        int j = 0;
        try
        {
            file = new StreamWriter(s);
            foreach (Car i in obj)
            {
                if (obj[j] != null) file.WriteLine(obj[j].manufacturer + ";" + obj[j].model + ";"
                                                   + obj[j].date.ToShortDateString() + ";" + obj[j].volume + ";" +
                                                   obj[j].power + ";" + obj[j].trance + ";");
                j++;
            }
            file.Flush();
        }
        catch (Exception exc)
        {
            Console.WriteLine("не удалось открыть файл {0} потому что {1}", s, exc);
        }
        finally { file.Close(); }
        file.Close();
    }

    public static void RemoveItem(int x)
    {
        Action.idx = x;
        Action.idx = Action.obj.FindIndex(Action.IsID);
        if (Action.idx >= 0)
        {
            Action.obj.RemoveAt(Action.idx);
            Console.WriteLine("объект с ID={0} удален", x);
            Action.idx = -1;

            return;
            }
        Console.WriteLine("не удалось найти объект с ID={0}", x);
    }

    public static void EditItem(int x, string s)
    {
        Action.idx = x;
        Action.idx = Action.obj.FindIndex(Action.IsID);
        if (Action.idx >= 0)
        {
            Action.obj[Action.idx] = new Car(s);
            Console.WriteLine("объект с ID={0} заменен", Action.idx);
            Action.idx = -1;
            return;
        }
        Console.WriteLine("не удалось найти объект с ID={0}", x);
    }
}

class Action
{
    public static int Id = 0;
    static string s = null;
    static string[] command;
    public static int idx;
    public static List<Car> obj = new List<Car>();
    public static Dictionary<int, string> TranceDic = new Dictionary<int, string>();

    public static void Help()
    {
        Console.WriteLine("Список комманд:");
        Console.WriteLine("<exit>                               - выход из программы");
        Console.WriteLine("<-load [путь к файлу]>               - загрузка данных из указанного файла\n" +
                          "в имени файла не должно быть пробелов");
        Console.WriteLine("<-list>                              - отображение текущих данных");
        Console.WriteLine("<-edit[ID];[новые параметры записи]> - замена всех свойств в строке #ID");
        Console.WriteLine("<-add [параметры записи]>            - добавление в список новой строки");
        Console.WriteLine("<-remove [ID]>                       - удаление строки из списка");
        Console.WriteLine("<-save>                              - сохранение файла");
        Console.WriteLine();

    }

    public static void Show(List<Car> obj)
    {
        foreach (Car o in obj)
            Console.WriteLine(o);
    }

    public static bool IsID(Car x)
    {
        return x.ID == idx;
    }

    static void CommandText()
    {
        bool flag = false;
        while (s != "exit")
        {
            if (!flag) Console.WriteLine("введите комманду. Для помощи введите <-help>");
            s = Console.ReadLine();
            command = s.Split(' ');
            flag = false;
            try
            {
                switch (command[0])
                {
                    case "exit": return;
                    case "-help": Action.Help(); break;
                    case "-load":
                        obj = textWork.OpenFile(command[1]);
                        break;


                    case "-add":
                        obj.Add(new Car(command[1]));
                        break;
                    case "-remove":
                        textWork.RemoveItem(Int32.Parse(command[1]));
                        break;
                    case "-save":
                        textWork.SaveFile(obj, command[1]);
                        break;
                    case "-list":
                        //Console.WriteLine("открыт файл " + command[1] + ". Вот его содержимое:\n");
                        Action.Show(obj);
                        break;
                    case "-edit":
                        textWork.EditItem(Int32.Parse(command[1]), command[2]);
                        break;
                    case "-sort":
                        obj.Sort();
                        break;
                    default: Console.WriteLine("некорректная комманда. Для списка комманд введите <-help>"); flag = true; break;

                }
            }
            catch (Exception) { Console.WriteLine("что то пошло не так"); }
        }

    }

    static void CommandBase()
    {
//заполнение словаря видов трансмиссий
        OleDbConnection con = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
        OleDbCommand comm = new OleDbCommand("SELECT * FROM Trancemission", con);
        try
        {
            con.Open();
            OleDbDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                TranceDic.Add(reader.GetInt32(0), reader.GetString(1));
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message.ToString()); }
        finally { con.Close(); }
//конец заполнения

        obj = baseWork.OpenBase();
        bool flag = false;
        while (s != "exit")
        {
            if (!flag) Console.WriteLine("введите комманду. Для помощи введите <-help>");
            s = Console.ReadLine();
            command = s.Split(' ');
            flag = false;
            try
            {
                switch (command[0])
                {
                    case "exit": return;
                    case "-help": Action.Help(); break;
                    case "-load":
                            obj = baseWork.OpenBase();
                        break;
                    case "-add":
                        baseWork.AddItem(command[1]);
                        break;
                    case "-remove":
                        baseWork.RemoveItem(Int32.Parse(command[1]));
                        break;
                    case "-list":
                        Action.Show(obj);
                        break;
                    case "-edit":
                        baseWork.EditItem(Int32.Parse(command[1]), command[2]);
                        break;
                    case "-sort":
                        obj.Sort();
                        break;
                    default: Console.WriteLine("некорректная комманда. Для списка комманд введите <-help>"); flag = true; break;

                }
            }
            catch (Exception) { Console.WriteLine("что то пошло не так"); }
        }

    }

    static void Main()
    {
        Console.WriteLine("Нажми b, если хочешь работать с базой или t если с файлом txt");
        if (Console.ReadLine()[0] == 'b')
            Action.CommandBase();
        else Action.CommandText();
    }
}


