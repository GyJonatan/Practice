using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validation.Classes;

namespace Validation
{
    class Person
    {
        [MaxLength(40)]
        public string Name { get; set; }
        [Range(0,200)]
        public int Height { get; set; }
    }
}
