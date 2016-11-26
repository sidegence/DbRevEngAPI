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
        public bool IsOutput { get; set; }
        public bool HasDefaultValue { get; set; }
        public string DefaultValue { get; set; }
    }

    public class ResultColumn
    {
        public int Ordinal { get; set; }
        public string Name { get; set; }
        public string SQLType { get; set; }
        public int SQLTypeSize { get; set; }
        public string SourceSchema { get; set; }
        public string SourceTable { get; set; }
        public string SourceColumn { get; set; }
    }
}
