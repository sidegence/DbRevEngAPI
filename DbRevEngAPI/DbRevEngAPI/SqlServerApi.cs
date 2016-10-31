using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;

using DbRevEngAPI.Entities;
using System.Collections;

namespace DbRevEngAPI
{
    public class SqlServerApi : BaseApi
    {
        public SqlServerApi(IDbConnection db) : base (db)
        {
        }

        public SqlServerApi(string connectionString) : base(connectionString)
        {
        }

        public override bool TestConnection()
        {
            try
            {
                var result = _db.Query<int>(@"
                    SELECT 1
                "
                ).Single();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override string Version()
        {
            return _db.Query<string>(@"
                SELECT @@VERSION
            "
            ).FirstOrDefault();
        }

        public override IEnumerable<Database> Databases()
        {
            var results = _db.Query<Database>(@"
                SELECT name 'Name', collation_name 'Collation'
                FROM sys.databases            
            "
            ).AsEnumerable();

            return results;
        }

        public override Database Database(string dbName)
        {
            var result = _db.Query<Database>(string.Format(@"
                SELECT name 'Name', collation_name 'Collation'
                FROM sys.databases   
                where name = '{0}'     
            ", dbName)
            ).FirstOrDefault();

            if (!Checker.IsNullOrEmpty(result))
            {
                result.Tables = Tables(result.Name);
                result.StoredProcedures = StoredProcedures(result.Name);
            }

            return result;
        }

        public override IEnumerable<Table> Tables(string dbName)
        {
            dbName = CorrectBracketsOnTheDbObject(dbName);

            var results = _db.Query<Table>(string.Format(@"
                use {0};
                select [name] 'Name', type 'Type'
                from sys.objects
                where [type] in ('U','V')          
            ", dbName)
            ).AsEnumerable();

            foreach (var item in results)
            {
                item.Columns = Columns(dbName, item.Name);
            }

            return results;
        }

        public override IEnumerable<Column> Columns(string dbName, string tableName)
        {
            dbName = CorrectBracketsOnTheDbObject(dbName);

            var results = _db.Query<Column>(string.Format(@"
                use {0};
                select 
	                c.column_id 'Ordinal', 
	                c.name 'Name', 
	                case when ic.column_id is null then 0 else 1 end 'IsPrimaryKey',
	                c.is_identity 'IsIdentity', 
	                t.name 'SQLType',  
	                c.max_length 'SQLTypeSize', 
	                case when fkc.parent_column_id is null then null else (select name from sys.objects where object_id=referenced_object_id) end 'FkTableName', 
	                case when fkc.parent_column_id is null then null else (select name from sys.columns where object_id=referenced_object_id and column_id=referenced_column_id) end 'FkColumnName'
                from sys.columns c
                inner join sys.objects o on o.object_id=c.object_id and o.name='{1}'
                inner join sys.types t on t.user_type_id=c.user_type_id
                left join sys.index_columns ic on ic.object_id=c.object_id and ic.column_id=c.column_id
                left join sys.foreign_key_columns fkc on fkc.parent_object_id=o.object_id and fkc.parent_column_id=c.column_id
                order by 1;
                ", dbName, tableName)
            ).AsEnumerable();

            foreach (var item in results)
            {
                if (!Checker.IsNullOrEmpty(item.FkTable))
                    if (!Checker.IsNullOrEmpty(item.FkColumnName))
                        item.FkColumn = Columns(dbName, item.FkTable).Single(c => c.Name == item.FkColumnName);
            }

            return results;
        }

        public override IEnumerable<StoredProcedure> StoredProcedures(string dbName)
        {
            dbName = CorrectBracketsOnTheDbObject(dbName);

            var results = _db.Query<StoredProcedure>(string.Format(@"
                use {0};
                select [name] 'Name', type 'Type'
                from sys.objects
                where [type] in ('P')          
            ", dbName)
            ).AsEnumerable();

            foreach (var item in results)
            {
                item.Parameters = Parameters(dbName, item.Name);
            }

            return results;
        }

        public override IEnumerable<Parameter> Parameters(string dbName, string storedProcedure)
        {
            dbName = CorrectBracketsOnTheDbObject(dbName);

            return _db.Query<Parameter>(string.Format(@"
                use {0};
                select  
                   parameter_id 'Ordinal',  
                   name 'Name',  
                   type_name(user_type_id) 'SQLType',  
                   max_length 'SQLTypeSize' 
                from sys.parameters p
                where object_id = object_id('{1}')
                ", dbName, storedProcedure)
            ).AsEnumerable();
        }
    }
}
