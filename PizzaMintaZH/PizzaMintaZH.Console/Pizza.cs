using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMintaZH
{
    class Pizza : IPizza
    {
        /*
         Feladat 1.
         Készítsen egy IPizza interfészt, ami az alábbi publikus tulajdonságokat írja elő:

         Készítsen egy Pizza osztályt, amely valósítsa meg az IPizza interfészt.

         Egészítse ki ezt az osztályt az alábbi tulajdonsággal:

         string FantasyName { get; set; }

         */

        [ToStringAttribute]
        public string Type { get; set; }
        [ToStringAttribute]
        public int Size { get; set; }
        public int PastaThickness { get; set; }
        public int NumberOfToppings { get; set; }
        [ToStringAttribute]
        public double Price { get; set; }
        [FantasyNameValidator('$', 10)] //Az attribútum osztályában a magyarázat erre
        [ToStringAttribute] 
        public string FantasyName { get; set; }


        /*
         Egészítse ki ezt az osztályt egy ToString metódussal, amely reflexió 
         segítségével a saját (jelölt) tulajdonságait és az aktuális értékét fogja 
         string formájában visszaadni. A jelöléshez hozza létre a megfelelő ToStringAttribute 
         osztályt a tanultaknak megfelelően, és tegye rá ezt az összes tulajdonságra (de tetszőlegesen 
         lehet variálni, hogy csak kevesebbre kerül rá).
         */
        public override string ToString()
        {
            string result = "";           
            foreach (PropertyInfo info in this.GetType().GetProperties()
               .Where(x => x.GetCustomAttribute<ToStringAttribute>() != null))
            {
                
                
                string name = info.Name;
                string value = info.GetValue(this)?.ToString();                    

                result += name + " = " + value + "\n";
                
            }
                     
            return result;
        }
    }
}
