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
        public string FkTableName { get; set; }
        public string FkColumnName { get; set; }

        public Table FkTable { get; set; }
        public Column FkColumn { get; set; }

        public override string ToString()
        {
            return string.Format(
                "{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, [{8}], [{9}]",
                Ordinal,
                Name, 
                IsPrimaryKey, 
                IsIdentity, 
                SQLType, 
                SQLTypeSize,
                FkTableName,
                FkColumnName,
                Checker.IsNullOrEmpty(FkTable) ? "" : FkTable.ToString(),
                Checker.IsNullOrEmpty(FkColumn) ? "" : FkColumn.ToString()
            );
        }
    }
}
