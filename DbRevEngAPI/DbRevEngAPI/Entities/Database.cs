using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbRevEngAPI.Entities
{
    public class Database
    {
        public string Name { get; set; }
        public string Collation { get; set; }

        public override string ToString()
        {
            return string.Format("Name:{0} Collation:{1}", Name, Collation);
        }
    }
}
