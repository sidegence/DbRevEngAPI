using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbRevEngAPI.Entities
{
    public class Column
    {
        public int  Ordinal { get; set; }
        public string Schema { get; set; }
        public string Name { get; set; }
        public bool IsPrimaryKey { get; set; } 
        public bool IsIdentity { get; set; }
        public bool IsNullable { get; set; }
        public string SQLType { get; set; }
        public int SQLTypeSize { get; set; }
        public int SQLTypePrecision { get; set; }
        public int SQLTypeScale { get; set; }
        public string FkTableName { get; set; }
        public string FkColumnName { get; set; }

        public Column FkColumn { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
