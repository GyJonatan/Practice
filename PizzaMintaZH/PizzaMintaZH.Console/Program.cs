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

                foreach (var item in doc.Descendants("Pizza"))
                {
                    list.Add(new Pizza()
                    {
                        FantasyName = item.Element("FantasyName").Value
                    });
                }

                return list;
            };

            IEnumerable<Pizza> pizzaList = xmlReader("pizza-database.xml");

            /*foreach (var item in pizzaList)
            {
                ConsoleLogger.ConsoleLog(item);
            }*/

            /*
            Feladat 6.
            Olvassa be a pizza-database.xml állományt és hajtsa végre rajta a következő lekérdezéseket:
            */

            XDocument doc = XDocument.Load("pizza-database.xml");

            //6.1. kérdezze le azokat a pizzákat amelyek fantázianevében nem szerepel a pizza szó
            var q1 = from x in doc.Descendants("Pizza")
                     where (x.Element("FantasyName").Value.ToLower().Contains("pizza"))
                     select x;

            q1.ToConsole("Q1");

            /*
             6.2. kérdezze le, hogy az egyes méretekből hány darab van, majd rendezze ezeket 
             darabszám alapján csökkenő sorrendbe (a kimenet egy új névtelen osztályban legyen 
             TYPE és COUNT mezőkkel)
             */
            var q2 = from x in doc.Descendants("Pizza")
                     group x by x.Element("Size").Value into grp
                     orderby grp.Count() descending
                     select new
                     {
                         TYPE = grp.Key,
                         COUNT = grp.Count()
                     };

            q2.ToConsole("Q2");


            //6.3.1. kérdezze le a pizzák nevét és típusát, amelyek legalább 4 feltéttel rendelkeznek

            var q3 = from x in doc.Descendants("Pizza")
                     where int.Parse(x.Element("NumberOfToppings").Value) >= 4
                     select new
                     {
                         FantasyName = x.Element("FantasyName").Value,
                         Type = x.Element("Type").Attribute("base").Value + " " + x.Element("Type").Value
                     };

            q3.ToConsole("Q3");

            //6.3.2. határozza meg, hogy mennyi ezeknek az átlagos ára típusonként

            var q3b = from x in doc.Descendants("Pizza")
                      group x by x.Element("Type").Value into g
                      let avg = g.Average(x => (int)x.Element("Price"))
                      select new
                      {
                          Type = g.Key,
                          AVG = avg
                      };

            q3b.ToConsole("Q3b");


            /*
             6.4. kérdezze le, hogy átlagosan mennyi az ára az egyes méretű 
             pizzáknak (a kimenet egy új névtelen osztályban legyen SIZE és AVGSAL mezőkkel)
             */
            var q4 = from x in doc.Descendants("Pizza")
                     group x by x.Element("Size") into grp
                     select new
                     {
                         SIZE = grp.Key,
                         AVGSAL = grp.Average(x => int.Parse(x.Element("Price").Value))
                     };

            q4.ToConsole("Q4");


            //6.5. határozza meg, hogy melyik pizzát éri meg megvenni a leginkább
            //(az ár & feltétek száma & méret paraméterek tekintetében)

            var q5 = from x in doc.Descendants("Pizza")
                     let price = (int)x.Element("Price")
                     let toppings = (int)x.Element("NumberOfToppings")
                     let size = (int)x.Element("Size")
                     //price / (toppings + size)
                     let asd = price / (toppings + size)
                     orderby asd descending
                     select x;
            
            q5.Take(1).ToConsole("Q5");

            /*
             6.6. határozza meg, hogy hány paradicsomos alapú pizza van amelyeknek 
             a feltétszáma kevesebb mint a második legtöbb feltéttel rendelkező pizza 
             (az összes közül) és amelyeknek az ára a középső ársávban található. középső 
             ársáv alatt a maximális ár és a minimális ár plusz/minusz 10%-át értjük
             */

            var descByToppings = doc.Descendants("Pizza")
                                .OrderByDescending(x => x.Element("NumberOfToppings").Value)
                                .Distinct()
                                .ToArray();

            var descByPrice = doc.Descendants("Pizza")
                            .OrderByDescending(x => x.Element("Price").Value)
                            .Select(x => (int)x.Element("Price"));

            var q6 = doc.Descendants("Pizza")
                     .Where(x => x.Element("Type").Attribute("base").Value.ToString() == "tomato")
                     .Where(x => int.Parse(x.Element("NumberOfToppings").Value)
                            < int.Parse(descByToppings[1].Element("NumberOfToppings").Value))
                     .Where(x => (int)x.Element("Price") <= (int)descByPrice.First() * 0.9 &&
                            (int)x.Element("Price") >= (int)descByPrice.Last() * 1.1);

            Console.WriteLine("Q6: " + q6.Count() + "\n");

            //Ha ezt olvasod megnyerted magadnak a jogot, hogy megcsináld a 6.7-et :)

            //6.7.1. módosítsa úgy az xml elemeit, hogy ahol "cm" attribútum érték található,
            //ott írja ki helyette (a fájlban), hogy "centimetres"




            //6.8. határozza meg, hogy az egyes méretekből típusonként hány darab található

            //var q8 = doc.Descendants("Pizza")
            //            .GroupBy(x => x.Element("Type"))
            //            .Select(x => x.Elements("Size"))
            //            .Count();

            //var q8b = from x in doc.Descendants("Pizza")
            //          group x by x.Element("Type") into g
            //          let t = g.Key
            //          group g by g.Elements("Size") into grp
            //          select new
            //          {
            //              Size = grp.Key,
            //              Type = grp.Count()
            //          };

            //var q8c = from x in doc.Descendants("Pizza")
            //          group x by x.Element("Size") into g
            //          let count = g.Elements("Type")
            //          select new { };

            //var q8d =
            //doc.Descendants("Pizza").GroupBy(pizzas => pizzas.Element("Type"),
            //(type, elements) =>
            //new
            //{
            //    Type = type,
            //    Size = elements.GroupBy(pizzas => pizzas.Element("Size"),
            //                            (size, amount) =>
            //                            new
            //                            {
            //                                Name = size,
            //                                Count = amount.Count()
            //                            })
            //});
            //Console.WriteLine("Q8");
            //foreach (var data in q8d)
            //{
            //    Console.WriteLine(data.Type.Value);

            //    foreach (var element in data.Size)
            //        Console.Write("  " + element.Name.Value + " = " + element.Count + "\n");
            //}
            //Console.WriteLine("Q8");




            //var q8a = from x in doc.Descendants("Pizza")
            //          group x by x.Element("Type") into g
            //          select new { Type = g.Key, Size = g.Elements("Size") };




            //Console.WriteLine($"*****Q8*****");
            //foreach (var type in q8a)
            //{
            //    Console.WriteLine("Type: " + type.Type);
            //    foreach (var size in q8a.GroupBy(x => x.Size))
            //    {
            //        Console.WriteLine($"Size: {size.Key}, Count: {size.Count()}");
            //    }
            //}
            //Console.WriteLine($"*****Q8*****");

            var q8 = from x in doc.Descendants("Pizza")
                     group x by x.Element("Type").Value into g
                     from y in (from x in g
                                group x by x.Element("Size").Value)
                     group y by g.Key;

            foreach (var outerGroup in q8)
            {
                Console.Write($"Type = {outerGroup.Key}\n");
                foreach (var innerGroup in outerGroup)
                {
                    Console.Write($"\t{innerGroup.Key}");

                    int counter = 0;
                    foreach (var innerGroupElement in innerGroup)
                    {
                        counter++;
                    }
                    Console.Write($"\tCount: {counter}\n");
                }
            }



            

            //q8d.ToConsole("Q8");

            //foreach (var item in doc.Descendants("Pizza")
            //            .GroupBy(x => x.Element("Type")))
            //{

            //    Console.WriteLine(item.Key
            //    + ": " + item.Select(x => x.Elements("Size"))
            //                 .Distinct()
            //                 .Count());
            //}

            //var q18 = from x in doc.Descendants("Pizza")
            //         group x by doc.Element("Size") into grp
            //         select grp;

            //foreach (var item in q8)
            //{
            //    Console.WriteLine($"Type: {item.Select(x => x.Element("Type").Value)} " +
            //        $"Size: {item.Select(x => x.Element("Size").Value)}");
            //}

            //q8.ToConsole("Q8");

            /*
             6.9. kérje le, hogy melyek azok a pizzák amelyeknek a neve 
             kevesebb karakterből áll mint a PastaThickness (tészta vastagság) és a NumberOfToppings 
             (feltétek száma) összegének a kétszerese
             */

            var q9 = from x in doc.Descendants("Pizza")
                     let thickness = int.Parse(x.Element("PastaThickness").Value)
                     let toppings = (int)x.Element("NumberOfToppings")
                     where (int)x.Element("FantasyName").Value.Length < (thickness + toppings) * 2
                     select new
                     {
                         Name = x.Element("FantasyName").Value
                     };

            q9.ToConsole("Q9");

            //6.11. határozza meg, hogy a VIP státusszal ellátott pizzák átlagára az olcsóbb,
            //vagy pedig a sima pizzák átlagára

            var q11a = doc.Descendants("Pizza")
                          .Where(x => x.Attribute("status")?.Value.ToString() == "VIP")
                          .Average(x => (int)x.Element("Price"));

            var q11b = doc.Descendants("Pizza")
                          .Where(x => x.Attribute("status")?.Value.ToString() != "VIP")
                          .Average(x => (int)x.Element("Price"));
            
            Console.WriteLine($"\nQ11:\nVIP: {q11a}  Basic: {q11b}\t" +
                $"Greater or equal: {(q11a > q11b ? "VIP" : "Basic")}");


            //6.12. határozza meg, hogy a VIP vagy sima pizzáknál fordul elő többször a 30-as méret

            int vip = 0, basic = 0;
            foreach (var p in doc.Descendants("Pizza").Where(x => (int)x.Element("Size") == 30))
            {
                if (p.Attribute("status")?.Value == "VIP") vip++;
                else basic++;                
            }
            Console.WriteLine($"\nQ12:\nVIP: {vip}  Basic: {basic}\t" +
                $"Greater or equal: {(vip > basic ? "VIP" : "Basic")}");

            //6.13. határozza meg, hogy a VIP vagy a sima pizzáknál fordul elő átlagosan a hosszabb elnevezés

            var q13a = doc.Descendants("Pizza")
                          .Where(x => x.Attribute("status")?.Value.ToString() == "VIP")
                          .Average(x => (int)x.Element("FantasyName").Value.Length);

            var q13b = doc.Descendants("Pizza")
                          .Where(x => x.Attribute("status")?.Value.ToString() != "VIP")
                          .Average(x => (int)x.Element("FantasyName").Value.Length);

           

            Console.WriteLine($"\nQ13:\nVIP: {q13a}  Basic: {q13b}\t" +
                $"Greater or equal: {(q13a > q13b ? "VIP" : "Basic")}");





            Console.ReadLine();
        }
    }
}
