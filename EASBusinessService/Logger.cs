using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EASBusinessService
{
    public static class Logger
    {
        public static void Debug(string message)
        {
            Console.WriteLine(message);
        }

        public static void Debug(string message, Exception exception)
        {
            Console.WriteLine($"{message} Exception: {exception.ToString()}");
        }
    }
}