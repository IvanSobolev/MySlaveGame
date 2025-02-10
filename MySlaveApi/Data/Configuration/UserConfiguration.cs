using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MySlaveApi.Model.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Настройка таблицы
        builder.ToTable("Users"); 

        // Первичный ключ
        builder.HasKey(u => u.Id);

        // Свойство UserName
        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(100);

        // Связь с Owner (самосвязь)
        builder.HasOne(u => u.Owner)
            .WithMany(u => u.SubUsers)
            .HasForeignKey(u => u.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Индексы
        builder.HasIndex(u => u.UserName).IsUnique();
        builder.HasIndex(u => u.OwnerId);
    }
}