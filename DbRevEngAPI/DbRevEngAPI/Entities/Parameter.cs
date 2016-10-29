using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbRevEngAPI.Entities
{
    public class Parameter
    {
        public int  Ordinal { get; set; }
        public string Name { get; set; }
        public string SQLType { get; set; }
        public int SQLTypeSize { get; set; }

        public override string ToString()
        {
            return string.Format(
                "{0}, {1}, {2}, {3}",
                Ordinal,
                Name, 
                SQLType, 
                SQLTypeSize
            );
        }
    }
}
