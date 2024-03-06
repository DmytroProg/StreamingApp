using Microsoft.EntityFrameworkCore;
using Server.DAL.Models;
using StreamingApp.BLL.Models;
namespace Server.DAL.Entities;

public class InMemoryDbContext : DbContext
{
    public InMemoryDbContext() : base()
    {

    }

    public DbSet<User> User { get; set; }
    public DbSet<Meeting> Meeting { get; set; }
    public DbSet<TextMessage> Message { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("InMemoryDb");
    }
}
