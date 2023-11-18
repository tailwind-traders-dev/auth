using Microsoft.EntityFrameworkCore;
using Tailwind.Auth.Data.Models;
namespace Tailwind.Auth.Data;
public class DB: DbContext
{

  public DB(DbContextOptions<DB> options) : base(options)
  {

  }

  public DbSet<User> Users { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
      modelBuilder.Entity<User>().ToTable("users");
  }
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    DotEnv.Load();
    var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
    optionsBuilder.UseNpgsql(connectionString);//.LogTo(Console.WriteLine, LogLevel.Information);
  }
}