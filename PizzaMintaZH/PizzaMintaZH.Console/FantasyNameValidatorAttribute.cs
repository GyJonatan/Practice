using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMintaZH
{
    /*
     Feladat 2.
     Készítsen egy FantasyNameValidatorAttribute osztályt, amely rendelkezzen a következő tulajdonságokkal:

     char Character { get; set; }
     int Length { get; set; }

     Az osztályra tegyen megszorítást, hogy csak tulajdonságokra lehessen alkalmazni. 
     Az előzőekben létrehozott (Pizza osztály) FantasyName tulajdonságra alkalmazza az attribútumot, 
     értéknek adja meg a $ karaktert, valamint a 10 értéket, mint hosszt.
     */
    [AttributeUsage(AttributeTargets.Property)]
    class FantasyNameValidatorAttribute : Attribute
    {
        public char Character { get; set; }
        public int Length { get; set; }
        public FantasyNameValidatorAttribute(char character, int length)
        {
            this.Character = character;
            this.Length = length;
        }

    }
}
