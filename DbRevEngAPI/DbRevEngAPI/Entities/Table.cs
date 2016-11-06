using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbRevEngAPI.Entities
{
    public class Table
    {
        public string Schema { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public IEnumerable<Column> Columns { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
