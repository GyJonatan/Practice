using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Linq;

namespace PizzaMintaZH
{
    /*
     Feladat 4.
     Készítsen egy Detector osztályt, benne egy void DetectPizzaClasses metódussal. 
     A metódus futásidőben vizsgálja meg reflexió segítségével az aktuális osztályokat, 
     ezek nevét kérje le fordított ABC sorrendbe rendezve egy tömbbe. Figyeljen, hogy csak 
     azokat az osztályokat kérje le, amelyek az IPizza interfészt megvalósítják. 
     A látványosabb teszteléshez készítsen a Pizza osztályból három darab leszármazottat 
     (VegaPizza, NagyPizza, BebiPizza). Ezekben további dolgok nem lesznek elhelyezve. 
     A lekért típusokat írja ki XML fájlba (pizzaClasses.xml néven) figyelve az XML struktúra betartására. 
     Írja ki az osztályokat nevét és a nevek hashkódját. A gyökérben attribútumként helyezze el, hogy hány 
     osztály van összesen.
     */
    public static class Detector
    {
        public static void DetectPizzaClasses()
        {
            var q = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(x => x.GetInterface(nameof(IPizza)) != null)
                    .OrderByDescending(x => x.FullName)
                    .ToArray();

            XDocument doc = new XDocument();
            doc.Add(new XElement("pizza",
                    new XAttribute("count", q.Length)));

            foreach (var item in q)
            {
                doc.Add(new XElement(new XElement("class",
                                        new XElement("name", item.Name),
                                        new XElement("hashCode", item.GetHashCode()))));
            }

            doc.Save("pizzaClasses.xml");
            
        }
    }
}
