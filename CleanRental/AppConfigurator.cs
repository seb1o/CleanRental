using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanRental
{
    internal class AppConfigurator
    {
        public static IConfiguration Configuration { get; private set; }
        static AppConfigurator()
        {
            if (Configuration == null)
            {
                Configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("json-conf.json", optional: false, reloadOnChange: true)
                    .Build();
            }
        }

        public static string? GetConnectionString()
        {
            return Configuration.GetConnectionString("Default");
            //return Configuration.GetSection("DBConfiguration")["DefaultConnectionString"];
        }
    }
}
