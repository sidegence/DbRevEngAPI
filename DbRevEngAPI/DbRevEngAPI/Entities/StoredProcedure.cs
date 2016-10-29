using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbRevEngAPI.Entities
{
    public class StoredProcedure
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public IEnumerable<Parameter> Parameters { get; set; }

        public override string ToString()
        {
            return string.Format(
                "{0}, {1}, [{2}]", 
                Name,
                Type,
                Parameters.Select(_=>_.ToString())
            );
        }
    }
}
