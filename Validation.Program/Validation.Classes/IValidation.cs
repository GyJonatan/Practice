using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Validation.Classes
{
    internal interface IValidation
    {
        bool Validate(object instance, PropertyInfo propertyInfo);
    }
}
