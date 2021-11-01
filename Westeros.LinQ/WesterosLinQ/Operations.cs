using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WesterosLinQ
{
    static class Operations
    {
        public static void ToConsole<T>(this IEnumerable<T> input, string header)
        {
            Console.WriteLine($"*****{header}*****");
            foreach (var item in input)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($"*****{header}*****");
            Console.ReadLine();
        }
    }
}
