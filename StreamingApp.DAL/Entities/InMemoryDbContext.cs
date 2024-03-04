using Microsoft.EntityFrameworkCore;
using Server.DAL.Models;
namespace Server.DAL.Entities;

public class InMemoryDbContext : DbContext
{
    public InMemoryDbContext()
    {

    }

    public DbSet<UserInfo> User { get; set; }
    public DbSet<MeetingInfo> Meeting { get; set; }
    public DbSet<TextMessageInfo> Message { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseInMemoryDatabase("InMemoryDb");
    }
}
