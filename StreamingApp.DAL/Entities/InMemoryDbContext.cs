using Microsoft.EntityFrameworkCore;
using Server.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Server.DAL.Entities;

public class InMemoryDbContext : DbContext
{
    public DbSet<UserInfo> User { get; set; }
    public DbSet<MeetingInfo> Meeting { get; set; }
    public DbSet<MessageBaseInfo> Message { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("InMemoryDb");
    }
}
