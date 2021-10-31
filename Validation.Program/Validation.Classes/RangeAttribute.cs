using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.Classes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RangeAttribute : Attribute
    {
        public int Min { get; private set; }
        public int Max { get; private set; }
        public RangeAttribute(int min = int.MinValue, int max = int.MaxValue)
        {
            this.Min = min;
            this.Max = max;
        }
    }
}
