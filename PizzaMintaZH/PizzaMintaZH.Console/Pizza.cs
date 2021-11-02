using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public override string ToString()
        {
            return
            ToStringAttributeHelper.GetNameAndValueIfMarked(this, nameof(Type)) +
            ToStringAttributeHelper.GetNameAndValueIfMarked(this, nameof(Size)) +
            ToStringAttributeHelper.GetNameAndValueIfMarked(this, nameof(PastaThickness)) +
            ToStringAttributeHelper.GetNameAndValueIfMarked(this, nameof(NumberOfToppings)) +
            ToStringAttributeHelper.GetNameAndValueIfMarked(this, nameof(Price)) +
            ToStringAttributeHelper.GetNameAndValueIfMarked(this, nameof(FantasyName));
        }
    }
}
