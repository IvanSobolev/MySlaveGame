using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySlaveApi.Model;

namespace MySlaveApi.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users"); 

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(u => u.Owner)
            .WithMany(u => u.SubUsers)
            .HasForeignKey(u => u.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(u => u.Id).IsUnique();
        builder.HasIndex(u => u.OwnerId);
    }
}