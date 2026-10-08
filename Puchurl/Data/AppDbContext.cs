using Microsoft.EntityFrameworkCore;
using Puchurl.Models;

namespace Puchurl.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Models.ShortUrl> ShortUrls => Set<ShortUrl>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShortUrl>(entity =>
        {
            entity.HasIndex(e => e.Code).IsUnique();
            entity.Property(e => e.Code).HasMaxLength(12);
        });
    }
}

