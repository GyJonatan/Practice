using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMintaZH
{
    /*
     Feladat 3.
     Készítsen egy Validator osztályt, amelyben egy bool CheckFantasyName metódus 
     segítségével vizsgálja meg, hogy a paraméternek kapott object rendelkezik-e FantasyName 
     tulajdonsággal, s amennyiben igen, úgy vizsgálja meg, hogy az attribútumban megadottaknak 
     eleget tesz-e az értéke, azaz rendelkezik-e a megadott karakterrel és van-e legalább olyan hosszú 
     karakterszámra. Ha igen, igaz értékkel térjen vissza, egyéb esetben hamissal. A feladat elvégzését 
     reflexióval valósítsa meg.
     */
    public static class Validator
    {
        public static bool CheckFantasyName(object obj, string propertyName)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);

            FantasyNameValidatorAttribute attribute = 
                propertyInfo.GetCustomAttribute<FantasyNameValidatorAttribute>();

            return attribute.Character == '$' && attribute.Length >= 10;
        }
    }
}
