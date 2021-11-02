using ConsoleLoggerLibrary;
using System;

namespace PizzaMintaZH
{
    /*
     Feladat 0.
     Hozzon létre egy class library projektet ConsoleLoggerLibrary néven. 
     Hozzon létre benne egy osztályt ConsoleLogger néven, benne egy void ConsoleLog metódust,
     amely egy object típust fogad. Az object-et írja ki a konzolra, a ToString metódus segítségével.
     Társítsa hozzá a másik (lentebbi következő) projekthez.
     */

    class Program
    {
        static void Main(string[] args)
        {
            Pizza pizza = new Pizza()
            {
                Type = "magyaros",
                Size = 32,
                PastaThickness = 5,
                NumberOfToppings = 4,
                Price = 1725,
                FantasyName = "epstein didnt kill himself"
            };

            Console.WriteLine(pizza.ToString());

            Console.ReadLine();
        }
    }
}
