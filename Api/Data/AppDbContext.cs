using Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public DbSet<Team> Teams { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Todo> Todos { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Связь между AppUser и Team (TeamId)
        modelBuilder.Entity<AppUser>()
            .HasOne(a => a.Team)
            .WithMany(t => t.Members)
            .HasForeignKey(a => a.TeamId)
            .OnDelete(DeleteBehavior.SetNull); // TeamId становится null при удалении команды

        // Связь между Team и Owner (OwnerId)
        modelBuilder.Entity<Team>()
            .HasOne(t => t.Owner)
            .WithMany()
            .HasForeignKey(t => t.OwnerId)
            .OnDelete(DeleteBehavior.Restrict); // Нельзя удалить владельца команды
    }
}