using Microsoft.EntityFrameworkCore;

namespace beltexam.Models
{
  public class beltexamContext : DbContext
  {
    public beltexamContext(DbContextOptions<beltexamContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<UserAndActivity> UsersAndActivities { get; set; }

  }
}