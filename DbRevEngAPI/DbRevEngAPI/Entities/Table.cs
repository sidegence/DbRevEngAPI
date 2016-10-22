using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbRevEngAPI.Entities
{
    public class Table
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return string.Format(
                "{0}, {1}", 
                Name,
                Type
            );
        }
    }
}
