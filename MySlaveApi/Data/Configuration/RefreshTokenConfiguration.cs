using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySlaveApi.Model;

namespace MySlaveApi.Data.Configuration;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");

        builder.HasKey(rt => rt.TgId);
        builder.Property(rt => rt.Token).IsRequired();
        builder.Property(rt => rt.Expires).IsRequired();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(rt => rt.TgId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}