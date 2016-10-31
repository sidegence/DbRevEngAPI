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
            var connectionStringSQLServer = ConfigurationManager.ConnectionStrings["SQLServer"].ConnectionString;
            var sqlServerApi = new SqlServerApi(connectionStringSQLServer);
            Console.WriteLine("\n qlServerApi(connectionStringSQLServer)");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine(connectionStringSQLServer);

            var testConnection = sqlServerApi.TestConnection();
            if (!testConnection)
            {
                Console.WriteLine("\n sqlServerApi.TestConnection() failed.");
                Environment.Exit(0);
            }

            Console.WriteLine("\n sqlServerApi.Version()");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine(sqlServerApi.Version());

            Console.WriteLine("\n sqlServerApi.Database(master)");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine(sqlServerApi.Database("master").ToString());

            Console.WriteLine("\n sqlServerApi.Tables(master)");
            Console.WriteLine("--------------------------------------------------------------------------------");
            foreach (var item in sqlServerApi.Tables("master"))
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\n sqlServerApi.Columns(master,spt_monitor)");
            Console.WriteLine("--------------------------------------------------------------------------------");
            foreach (var item in sqlServerApi.Columns("master", "spt_monitor"))
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\n sqlServerApi.StoredProcedures(master)");
            Console.WriteLine("--------------------------------------------------------------------------------");
            foreach (var item in sqlServerApi.StoredProcedures("master"))
            {
                Console.WriteLine(item.ToString());
            }

            Console.ReadKey();
        }
    }
}
