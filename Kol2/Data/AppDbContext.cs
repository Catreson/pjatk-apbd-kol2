using Kol2.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kol2.Data;

public class AppDbContext : DbContext
{
    // ↓ ONE LINE PER ENTITY
    public DbSet<Client> Clients { get; set; }
    public DbSet<Mechanic>  Concerts  { get; set; }
    public DbSet<Service>   Services   { get; set; }
    public DbSet<Visit>   Visits   { get; set; }


    protected AppDbContext() { }
    public AppDbContext(DbContextOptions options) : base(options) { }
}