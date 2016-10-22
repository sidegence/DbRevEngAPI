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
            var api = new DbRevEngAPI(connectionStringSQLServer);

            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine(api.Version());

            Console.WriteLine("Databases:");
            Console.WriteLine("--------------------------------------------------------------------------------");
            foreach (var item in api.Databases())
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("tables:");
            Console.WriteLine("--------------------------------------------------------------------------------");
            foreach (var item in api.Tables("master"))
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("columns:");
            Console.WriteLine("--------------------------------------------------------------------------------");
            foreach (var item in api.Columns("PixAlert.Licensing", "license"))
            {
                Console.WriteLine(item.ToString());
            }

            Console.ReadKey();
        }
    }
}
