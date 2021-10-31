using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Validation.Classes
{
    internal class MaxLengthValidation : IValidation
    {
        MaxLengthAttribute lengthAttribute;
        public MaxLengthValidation(MaxLengthAttribute lengthAttribute)
        {
            this.lengthAttribute = lengthAttribute;
        }
        public bool Validate(object instance, PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType == typeof(string))
            {
                var value = (string)propertyInfo.GetValue(instance);
                return value.Length <= lengthAttribute.Length;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
