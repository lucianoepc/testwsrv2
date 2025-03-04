using Microsoft.EntityFrameworkCore;
using PoC.TestWServ2.Common.Entities;

namespace PoC.TestWSrv2.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Person> People { get; set; }
}