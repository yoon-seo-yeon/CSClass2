using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSClass2
{
    class Wanted <T>
    {
        public T Value;
        public Wanted(T value)
        {
            this.Value = value;
        }
    }
}
