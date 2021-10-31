using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Validation.Classes
{
    public class Validator
    {
        public bool Validate(object instance)
        {
            ValidationFactory validationFactory = new ValidationFactory();

            PropertyInfo[] properties = instance.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                var allAttributes = propertyInfo.GetCustomAttributes();
                foreach (Attribute attribute in allAttributes)
                {
                    IValidation validation = validationFactory.GetValidation(attribute);
                    if (validation?.Validate(instance, propertyInfo) == false)
                    {
                        return false;
                    }
                }

            }
            return true;
        }
    }
}
