using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace violet.backend.Entities.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.UseTptMappingStrategy();

        builder.Property(x => x.Username)
            .IsRequired(false)
            .HasMaxLength(200)
            .IsUnicode();
        builder.Property(x => x.Password)
            .IsRequired(false)
            .HasMaxLength(200)
            .IsUnicode();

        builder.OwnsOne(x => x.TelegramInfo, ti =>
        {
            ti.Property(p => p.FirstName).IsRequired().IsUnicode().HasMaxLength(200);
            ti.Property(p => p.LastName).IsRequired(false).IsUnicode().HasMaxLength(200);
            ti.Property(p => p.Username).IsRequired(false).IsUnicode().HasMaxLength(200);
            ti.Property(p => p.LanguageCode).IsRequired(false).IsUnicode(false).HasMaxLength(50);
            ti.Property(p => p.PhotoUrl).IsRequired(false).IsUnicode(false).HasMaxLength(500);
        });
    }
}