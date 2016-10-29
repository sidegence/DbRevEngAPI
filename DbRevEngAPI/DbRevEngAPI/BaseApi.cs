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
    public abstract class BaseApi
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

        public virtual string Version()
        {
            return null;
        }

        public virtual Database Database(string dbName)
        {
            return null;
        }

        public virtual IEnumerable<Database> Databases()
        {
            return null;
        }

        public virtual IEnumerable<Table> Tables(string dbName)
        {
            return null;
        }

        public virtual IEnumerable<Column> Columns(string dbName, string tableName)
        {
            return null;
        }

        public virtual IEnumerable<StoredProcedure> StoredProcedures(string dbName)
        {
            return null;
        }

        public virtual IEnumerable<Parameter> Parameters(string dbName, string storedProcedure)
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
