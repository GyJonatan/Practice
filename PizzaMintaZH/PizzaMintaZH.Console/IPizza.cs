using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMintaZH
{
    /*
     Feladat 1.
     Készítsen egy IPizza interfészt, ami az alábbi publikus tulajdonságokat írja elő:

            string Type { get; set; }
            int Size { get; set; }
            int PastaThickness { get; set; }
            int NumberOfToppings { get; set; }
            double Price {get; set; }
     */
    interface IPizza
    {
        public string Type { get; set; }
        public int Size { get; set; }
        public int PastaThickness { get; set; }
        public int NumberOfToppings { get; set; }
        public double Price { get; set; }

    }
}
