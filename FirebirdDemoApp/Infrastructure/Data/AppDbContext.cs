using FirebirdDemoApp.Vehicles.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirebirdDemoApp.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Vehicle> Vehicles { get; set; }
}