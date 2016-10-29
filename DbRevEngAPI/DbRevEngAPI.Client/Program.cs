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
            var api = new SqlServerApi(connectionStringSQLServer);

            Console.WriteLine("\n api.Version()");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine(api.Version());

            Console.WriteLine("\n api.Databases()");
            Console.WriteLine("--------------------------------------------------------------------------------");
            foreach (var item in api.Databases())
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\n api.Tables(\"master\")");
            Console.WriteLine("--------------------------------------------------------------------------------");
            foreach (var item in api.Tables("master"))
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\n api.Columns(\"master\", \"spt_monitor\")");
            Console.WriteLine("--------------------------------------------------------------------------------");
            foreach (var item in api.Columns("master", "spt_monitor"))
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\n api.Columns(\"PixAlert.Licensing\", \"License\")");
            Console.WriteLine("--------------------------------------------------------------------------------");
            foreach (var item in api.Columns("PixAlert.Licensing", "License"))
            {
                Console.WriteLine(item.ToString());
            }

            Console.ReadKey();
        }
    }
}
