using ConsoleLoggerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace PizzaMintaZH
{
    
    class Program
    {
        static void Main(string[] args)
        {

            /*
     
             Hozzon létre egy Func delegáltat, amely egy fájl nevet kap bemenetnek (string) és
             egy IEnumerable<Pizza> típussal tér vissza. A delegáltba hozzon létre egy névtelen függvényt,
             amelyben a kapott fájlt (pizzas.xml ld. később) beolvassa és egy List-et állít elő. Elegendő csak
             a fantázianeveket kiválasztani a Pizza objektumok előállításakor. Ezt követően hívja meg a delegáltat
             és az előállt kimenetet validálja le fantázianevek alapján. Az eredményt a korábban létrehozott class 
             libraryban definiált ConsoleLogger segítségével írassa ki.     

             */
            Func<string, IEnumerable<Pizza>> xmlReader = x =>
            {
                var doc = XDocument.Load(x);
                List<Pizza> list = new List<Pizza>();

                foreach (var item in doc.Root.Descendants("pizza"))
                {
                    list.Add(new Pizza()
                    {
                        FantasyName = item.Element("FantasyName").Value
                    });
                }

                return list;
            };

            IEnumerable<Pizza> pizzaList = xmlReader("pizza-database.xml");
            ConsoleLogger.ConsoleLog(pizzaList);

            /*
            Feladat 6.
            Olvassa be a pizza-database.xml állományt és hajtsa végre rajta a következő lekérdezéseket:
            */

            XDocument doc = XDocument.Load("pizza-database.xml");

            //6.1. kérdezze le azokat a pizzákat amelyek fantázianevében nem szerepel a pizza szó
            var q1 = from x in doc.Descendants("PizzaDatabase")
                     where !x.Element("FantasyName").Value.ToLower().Contains("pizza")
                     select x;

            ConsoleLogger.ConsoleLog(q1);

            /*
             6.2. kérdezze le, hogy az egyes méretekből hány darab van, majd rendezze ezeket 
             darabszám alapján csökkenő sorrendbe (a kimenet egy új névtelen osztályban legyen 
             TYPE és COUNT mezőkkel)
             */
            var q2 = from x in doc.Descendants("PizzaDatabase")
                     group x by x.Element("Size").Value into grp
                     orderby grp.Count() descending
                     select new
                     {
                         TYPE = grp.Key,
                         COUNT = grp.Count()
                     };
            ConsoleLogger.ConsoleLog(q2);


            //6.3.1. kérdezze le a pizzák nevét és típusát, amelyek legalább 4 feltéttel rendelkeznek

            var q3 = from x in doc.Descendants("PizzaDatabase")
                     where int.Parse(x.Element("NumberOfToppings").Value) >= 4
                     select new
                     {
                         Name = x.Element("Name").Value,
                         Type = x.Element("Type").Value
                     };

            ConsoleLogger.ConsoleLog(q3);

            /*
             6.4. kérdezze le, hogy átlagosan mennyi az ára az egyes méretű 
             pizzáknak (a kimenet egy új névtelen osztályban legyen SIZE és AVGSAL mezőkkel)
             */
            var q4 = from x in doc.Descendants("PizzaDatabase")
                     group x by x.Element("Size") into grp
                     select new
                     {
                         SIZE = grp.Key,
                         AVGSAL = grp.Average(x => int.Parse(x.Element("Price").Value))
                     };


            ConsoleLogger.ConsoleLog(q4);




            Console.ReadLine();
        }
    }
}
