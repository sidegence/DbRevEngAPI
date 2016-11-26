using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using DbRevEngAPI;

namespace DbRevEngAPI.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbName = "master";
            var connectionStringSQLServer = ConfigurationManager.ConnectionStrings["SQLServer"].ConnectionString;
            var sqlServerApi = new SqlServerApi(connectionStringSQLServer);
            Console.WriteLine("\n qlServerApi(connectionStringSQLServer)");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine(connectionStringSQLServer);
            Console.WriteLine("dbName:"+dbName);

            var testConnection = sqlServerApi.TestConnection();
            if (!testConnection)
            {
                Console.WriteLine("\n sqlServerApi.TestConnection() failed.");
                Environment.Exit(0);
            }

            Console.WriteLine("\n sqlServerApi.Version()");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine(sqlServerApi.Version());

            var database = sqlServerApi.Database(dbName, "%", "%");
            Console.WriteLine("\n sqlServerApi.Database(dbName, \"%\", \"%\")");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine(database.ToString());

            Console.WriteLine("\n database.Tables()");
            Console.WriteLine("--------------------------------------------------------------------------------");
            foreach (var item in database.Tables)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\n database.StoredProcedures()");
            Console.WriteLine("--------------------------------------------------------------------------------");
            foreach (var item in database.StoredProcedures)
            {
                Console.WriteLine(item.Name+" p:"+item.Parameters.Count()+" r:"+item.ResultColumns.Count());
            }

            Console.ReadKey();
        }
    }
}
