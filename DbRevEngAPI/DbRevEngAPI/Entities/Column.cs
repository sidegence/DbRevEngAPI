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
        public string Name { get; set; }
        public int IsPrimaryKey { get; set; } 
        public int IsIdentity { get; set; }
        public string SQLType { get; set; }
        public int SQLTypeSize { get; set; }
        public string FkTable { get; set; }
        public string FkColumn { get; set; }

        public override string ToString()
        {
            return string.Format(
                "{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}",
                Ordinal,
                Name, 
                IsPrimaryKey, 
                IsIdentity, 
                SQLType, 
                SQLTypeSize,
                FkTable, 
                FkColumn 
            );
        }
    }
}
