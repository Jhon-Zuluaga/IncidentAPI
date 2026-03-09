using Microsoft.EntityFrameworkCore;
using IncidentAPI.Api.Models;

namespace IncidentAPI.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Incident>()
            .Property(i => i.Status)
            .HasDefaultValue("abierto");

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Admin", Email = "admin@test.com"}
        );

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Hardware"},
            new Category { Id = 2, Name = "Software"},
            new Category { Id = 3, Name = "Red"}
        );
    }
    
}