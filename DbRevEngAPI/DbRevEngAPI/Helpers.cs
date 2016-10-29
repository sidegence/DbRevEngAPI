using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbRevEngAPI
{
    public static class Checker
    {
        public static bool IsNullOrEmpty<T>(T t)
        {
            return t == null || Equals(t, default(T));
        }
    }
}
