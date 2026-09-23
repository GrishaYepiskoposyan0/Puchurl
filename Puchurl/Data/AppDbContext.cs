using Microsoft.EntityFrameworkCore;
using Puchurl.Models;

namespace Puchurl.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Models.ShortUrl> ShortUrls => Set<ShortUrl>();
}

