using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QansBAL.Helper
{
    public static class StringHelper
    {
        public static string GetStorageKey(this string str )
        {
            return str.Trim().ToLower().Replace( ' ', '_' );
        }
    }
}
