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
        id = Action.Id++;
        if (sub.Length != 7)
        {
            Console.WriteLine("обнаружена некорректная строка, создан объект:");
            man = "некорректно";
            mod = "некорректно";
            return;
        }
        man = sub[0];
        mod = sub[1];
        d = DateTime.Parse(sub[2]);
        vol = Double.Parse(sub[3]);
        pow = Int32.Parse(sub[4]);
        if (sub[5] == "AT") tra = trancemission.AT;
        else tra = trancemission.MT;        
    }

    public override string ToString()
    {
        string result =String.Format("ID={6} {0} , {1} , {2}, v={3:N1}, p={4}, Trancemission is {5}",manufacturer,model,date.ToLongDateString(),volume,power,trance,ID);
        return result;
    }

}

class Action
{
    public static int Id = 0;
    static string s = null;
    static string[] command;
    static int idx;
    public static List<Car> obj = new List<Car>();
    static bool TB = false;

    static List<Car> OpenFile(string s)
    {
        obj.Clear();
        Id = 0;
        StreamReader file = null;
        try
        {
            file = new StreamReader(s);
            Console.WriteLine("Файл {0} открыт", s);
            string temp = file.ReadLine();
            while (temp != null)
            {
                obj.Add(new Car(temp));
                temp = file.ReadLine();
                
            }
        }
        catch (Exception exc)
        {
            Console.WriteLine("не удалось открыть файл {0} потому что {1}", s,exc);
        }
        finally { file.Close(); }
        return obj;
    }

    static List<Car> OpenBase()
    {
        obj.Clear();
        Id = 0;
        OleDbConnection con = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
        OleDbCommand command = new OleDbCommand("SELECT * FROM Car", con);
        try {
            con.Open();
            Console.WriteLine();

/*я просто напишу здесь всякой чуши, чтобы были изменения и было, что отправлять, а то что-то не получается!*/
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string str = reader.GetString(0) + ";" + reader.GetString(1) + ";" + reader.GetDateTime(2).ToShortDateString() + ";" + reader.GetDouble(3) + ";" + reader.GetInt32(4) + ";"+ reader.GetString(5)+";";
                obj.Add(new Car(str));
            }

            Console.WriteLine("Информация из БД успешно загружена");

        }

        catch (Exception ex) { Console.WriteLine(ex.Message.ToString()); }
        finally { con.Close(); }
        return obj;
    }

    static void SaveFile(List<Car> obj, string s)
    {
        StreamWriter file = null;
        int j = 0;
        try
        {
            file = new StreamWriter(s);
            foreach (Car i in obj) {
                if (obj[j] != null) file.WriteLine(obj[j].manufacturer+ ";" + obj[j].model+";" 
                                                   + obj[j].date.ToShortDateString() + ";" + obj[j].volume + ";" + 
                                                   obj[j].power + ";" + obj[j].trance+";");
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

    public static void Show(List<Car> obj)
    {
        foreach (Car o in obj)
            Console.WriteLine(o);
    }

    static void RemoveItem(int x)
    {
        idx = x;
        idx = obj.FindIndex(Action.IsID);
        if (idx >= 0)
        {
            obj.RemoveAt(idx);
            Console.WriteLine("объект с ID={0} удален", idx);
        }
        else Console.WriteLine("не удалось найти объект с ID={0}", x);
        idx = -1;

    }

    static void EditItem(int x, string s)
    {
        idx = x;
        idx = obj.FindIndex(Action.IsID);
        if (idx >= 0)
        {
            obj[idx] = new Car(s);
            Console.WriteLine("объект с ID={0} заменен", idx);
        }
        else Console.WriteLine("не удалось найти объект с ID={0}", x);
        idx = -1;

    }

    public static bool IsID(Car x)
    {
        return x.ID == idx;
    }

    static void Main()
    {
        bool flag = false;
        Console.WriteLine("Нажми b, если хочешь работать с базой или t если с файлом txt");
        if (Console.ReadLine()[0] == 'b')
            TB = true;

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
                    case "-help": Car.Help(); break;
                    case "-load":
                        if (TB == true)
                            obj = OpenBase();
                        else obj = OpenFile(command[1]);
                        break;


                    case "-add":
                        obj.Add(new Car(command[1]));
                        break;
                    case "-remove":
                        RemoveItem(Int32.Parse(command[1]));
                        break;
                    case "-save":
                        SaveFile(obj, command[1]);
                        break;
                    case "-list":
                        //Console.WriteLine("открыт файл " + command[1] + ". Вот его содержимое:\n");
                        Action.Show(obj);
                        break;
                    case "-edit":
                        EditItem(Int32.Parse(command[1]), command[2]);
                        break;
                    case "-sort":
                        obj.Sort();
                        break;
                    default: Console.WriteLine("некорректная комманда. Для списка комманд введите <-help>"); flag = true; break;

                }
            }
            catch (Exception exc) { Console.WriteLine("что то пошло не так: " + exc.Message); }
        }
    }
}


