using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMintaZH
{
    /*
     Egészítse ki ezt az osztályt egy ToString metódussal, 
     amely reflexió segítségével a saját (jelölt) tulajdonságait és az aktuális 
     értékét fogja string formájában visszaadni. A jelöléshez hozza létre a megfelelő 
     ToStringAttribute osztályt a tanultaknak megfelelően, és tegye rá ezt az összes tulajdonságra 
     (de tetszőlegesen lehet variálni, hogy csak kevesebbre kerül rá).
     */

    public static class ToStringAttributeHelper
    {
        

        public static string GetNameAndValueIfMarked(object obj, string propertyName)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);

            object attribute = propertyInfo.GetCustomAttributes(typeof(ToStringAttribute),false)
                                           .FirstOrDefault();

            if (attribute != null)
            {
                string name = propertyInfo.Name;
                string value = propertyInfo.GetValue(obj).ToString();


                return name + " = " + value + "\n";
            }
            else return string.Empty;


        }
    }
}
