using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.Classes
{
    internal class ValidationFactory
    {
        public IValidation GetValidation(Attribute attribute)
        {
            if (attribute is MaxLengthAttribute)
            {
                return new MaxLengthValidation(attribute as MaxLengthAttribute);
            }
            if (attribute is RangeAttribute)
            {
                return new RangeValidation(attribute as RangeAttribute);
            }
            return null;
        }
    }
}
