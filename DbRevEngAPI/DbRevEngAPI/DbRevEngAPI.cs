using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DbRevEngAPI
{
    public class DbRevEngAPI
    {
        private readonly IDbConnection _db;

        public DbRevEngAPI(IDbConnection db)
        {
            _db = db;
        }
    }
}
