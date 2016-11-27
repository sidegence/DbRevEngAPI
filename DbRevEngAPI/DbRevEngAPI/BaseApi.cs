using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DbRevEngAPI.Entities;

namespace DbRevEngAPI
{
    public abstract class BaseApi : IDisposable
    {
        protected readonly IDbConnection _db;

        public BaseApi(IDbConnection db)
        {
            _db = db;
        }

        public BaseApi(string connectionString)
        {
            _db = new SqlConnection(connectionString);
        }

        public void Dispose()
        {
            if (!Checker.IsNullOrEmpty(_db)) _db.Dispose();
        }

        public virtual bool TestConnection()
        {
            return false;
        }

        public virtual string Version()
        {
            return null;
        }

        public virtual Database Database(
            string dbName, 
            string tableNamePattern, string tableNameExceptPattern, 
            string procedureNamePattern, string procedureNameExceptPattern
        )
        {
            return null;
        }

        public virtual IEnumerable<Database> Databases()
        {
            return null;
        }

        public virtual IEnumerable<Table> Tables(string dbName, string tableNamePattern, string tableNameExceptPattern)
        {
            return null;
        }

        public virtual IEnumerable<Column> Columns(string dbName, string tableName)
        {
            return null;
        }

        public virtual IEnumerable<StoredProcedure> StoredProcedures(string dbName, string procedureNamePattern, string procedureNameExceptPattern)
        {
            return null;
        }

        public virtual IEnumerable<Parameter> Parameters(string dbName, StoredProcedure storedProcedure)
        {
            return null;
        }

        public virtual IEnumerable<ResultColumn> ResultColumns(string dbName, StoredProcedure storedProcedure)
        {
            return null;
        }

        protected string CorrectBracketsOnTheDbObject(string dbObject)
        {
            if (!dbObject.StartsWith("["))
                dbObject = string.Format("[{0}]", dbObject);
            return dbObject;
        }
    }
}
