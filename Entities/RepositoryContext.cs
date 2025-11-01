using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Owner> Owners { get; set; }
    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Owner>()
            .HasMany(o => o.Accounts)
            .WithOne(a => a.Owner)
            .HasForeignKey(a => a.OwnerId);

        base.OnModelCreating(modelBuilder);
    }
}
