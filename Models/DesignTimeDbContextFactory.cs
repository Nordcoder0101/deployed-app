
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace beltexam.Models
{
  public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<beltexamContext>
  {
    public beltexamContext CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();
      var builder = new DbContextOptionsBuilder<beltexamContext>();
      var connectionString = configuration.GetConnectionString("DefaultConnection");
      builder.UseSqlServer(connectionString);
      return new beltexamContext(builder.Options);
    }
  } 
}