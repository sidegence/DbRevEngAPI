using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbRevEngAPI.Entities
{
    public class StoredProcedure
    {
        public string Schema { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public IEnumerable<Parameter> Parameters { get; set; }
        public IEnumerable<ResultColumn> ResultColumns { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
