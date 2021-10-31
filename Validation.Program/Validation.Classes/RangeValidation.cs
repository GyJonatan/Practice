using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Validation.Classes
{
    internal class RangeValidation : IValidation
    {
        RangeAttribute rangeAttribute;
        public RangeValidation(RangeAttribute rangeAttribute)
        {
            this.rangeAttribute = rangeAttribute;
        }
        public bool Validate(object instance, PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType == typeof(int))
            {
                var value = (int)propertyInfo.GetValue(instance);
                return value <= rangeAttribute.Max && value >= rangeAttribute.Min;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
