using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMintaZH
{
    //Ez csak egy saját Attribútum

    [AttributeUsage(AttributeTargets.Property)]
    class ToStringAttribute : Attribute
    {       
    }
}
