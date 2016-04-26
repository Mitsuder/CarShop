using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientApp.WcfTest;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WcfTest.Service1Client client = new Service1Client();

            Console.WriteLine("введите строку для передачи сервису");
            string s = Console.ReadLine();
            Console.WriteLine("ответ сервиса:" + client.RemoveSpaces(s));
            Console.ReadKey();
        }
    }
}
