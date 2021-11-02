using System;

namespace ConsoleLoggerLibrary
{
    public class ConsoleLogger
    {
        public static void ConsoleLog(object obj)
        {
            Console.WriteLine(obj.ToString());
        }

        
    }
}
