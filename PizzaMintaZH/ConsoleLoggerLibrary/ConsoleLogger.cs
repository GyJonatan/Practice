using System;
using System.Collections.Generic;

namespace ConsoleLoggerLibrary
{
    /*
     Feladat 0.
     Hozzon létre egy class library projektet ConsoleLoggerLibrary néven. 
     Hozzon létre benne egy osztályt ConsoleLogger néven, benne egy void ConsoleLog metódust,
     amely egy object típust fogad. Az object-et írja ki a konzolra, a ToString metódus segítségével.
     Társítsa hozzá a másik (lentebbi következő) projekthez.
     */
    public static class ConsoleLogger
    {
        public static void ConsoleLog(object obj)
        {
            Console.WriteLine(obj.ToString());
        }
        public static void ToConsole<T>(this IEnumerable<T> input, string header)
        {
            Console.WriteLine($"*****{header}*****");
            foreach (var item in input)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine($"*****{header}*****");
            Console.ReadLine();
        }

    }
}
