using Microsoft.EntityFrameworkCore;
using MySlaveApi.Model;

namespace MySlaveApi.Data;

public class TokenContext(DbContextOptions<TokenContext> options) : DbContext(options)
{
    public DbSet<RefreshToken?> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(rt => rt.TgId); 
            entity.Property(rt => rt.Token).IsRequired();
            entity.Property(rt => rt.Expires).IsRequired();
        });
    }
}