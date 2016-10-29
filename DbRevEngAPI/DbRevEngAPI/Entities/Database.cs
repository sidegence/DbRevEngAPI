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

        public IEnumerable<Table> Tables { get; set; }
        public IEnumerable<StoredProcedure> StoredProcedures { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", Name, Collation, Tables, StoredProcedures);
        }
    }
}
