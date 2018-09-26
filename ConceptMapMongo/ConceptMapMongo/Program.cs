using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DotNetEnv;

namespace ConceptMapMongo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try 
            {
                DotNetEnv.Env.Load("./machine_config/machine.env");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine(System.Environment.GetEnvironmentVariable("MACHINE_LOCAL_IPV4"));
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
