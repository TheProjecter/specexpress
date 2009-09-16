using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecExpress.Util
{
    public static class ObjectExtensions
    {
        public static bool IsNullOrDefault<TProperty>(this TProperty input)
        {
            return !(input == null
                         || input.Equals(string.Empty)
                         || (input is ValueType && Equals(input, 0)
                         || !(  !(input is IEnumerable) ||
                                (input is IEnumerable && ((IEnumerable) (input)).GetEnumerator().MoveNext())))
                     );
            
        }

    }
}
