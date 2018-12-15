using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;


using Microsoft.EntityFrameworkCore.Design;

using beltexam.Models;

namespace beltexam
{
   public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
  }

}


  
//     public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<beltexamContext>
//     {
//       public beltexamContext CreateDbContext(string[] args)
//       {
//         IConfigurationRoot configuration = new ConfigurationBuilder()
//             .SetBasePath(Directory.GetCurrentDirectory())
//             .AddJsonFile("appsettings.json")
//             .Build();
//         var builder = new DbContextOptionsBuilder<beltexamContext>();
//         var connectionString = configuration.GetConnectionString("DefaultConnection");
//         builder.UseSqlServer(connectionString);
//         return new beltexamContext(builder.Options);
//       }
//     }
//   }

