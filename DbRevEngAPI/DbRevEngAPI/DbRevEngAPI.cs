using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;

using DbRevEngAPI.Entities;

namespace DbRevEngAPI
{
    public class DbRevEngAPI
    {
        private readonly IDbConnection _db;

        public DbRevEngAPI(IDbConnection db)
        {
            _db = db;
        }

        public DbRevEngAPI(string connectionString)
        {
            _db = new SqlConnection(connectionString);
        }

        public string Version()
        {
            return _db.Query<string>(@"
                SELECT @@VERSION
            "
            ).FirstOrDefault();
        }

        public IEnumerable<Database> Databases()
        {
            return _db.Query<Database>(@"
                SELECT name 'Name', collation_name 'Collation'
                FROM sys.databases            
            "
            ).AsQueryable();
        }

        public IEnumerable<Table> Tables(string dbName)
        {
            return _db.Query<Table>(string.Format(@"
                select name 'Name', type 'Type'
                from {0}.sys.objects
                where [type] in ('U','V')          
            ", dbName)
            ).AsQueryable();
        }

        public IEnumerable<Column> Columns(string dbName, string tableName)
        {
            return _db.Query<Column>(string.Format(@"
                use [{0}];
                select 
	                c.column_id 'Ordinal', 
	                c.name 'Name', 
	                case when ic.column_id is null then 0 else 1 end 'IsPrimaryKey',
	                c.is_identity 'IsIdentity', 
	                t.name 'SQLType',  
	                c.max_length 'SQLTypeSize', 
	                case when fkc.parent_column_id is null then null else (select name from sys.objects where object_id=referenced_object_id) end 'FkTable', 
	                case when fkc.parent_column_id is null then null else (select name from sys.columns where object_id=referenced_object_id and column_id=referenced_column_id) end 'FkColumn'
                from sys.columns c
                inner join sys.objects o on o.object_id=c.object_id and o.name='{1}'
                inner join sys.types t on t.user_type_id=c.user_type_id
                left join sys.index_columns ic on ic.object_id=c.object_id and ic.column_id=c.column_id
                left join sys.foreign_key_columns fkc on fkc.parent_object_id=o.object_id and fkc.parent_column_id=c.column_id
                order by 1;
                ", dbName, tableName)
            ).AsQueryable();
        }
    }
}
