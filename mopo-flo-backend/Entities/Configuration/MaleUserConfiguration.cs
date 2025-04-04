using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace violet.backend.Entities.Configuration;

public class MaleUserConfiguration : IEntityTypeConfiguration<MaleUser>
{
    public void Configure(EntityTypeBuilder<MaleUser> builder)
    {
        builder.ToTable("MaleUsers")
            .UseTptMappingStrategy();

        builder.OwnsOne(x => x.MaleProfile, pr =>
        {
            pr.Property(p => p.Weigh).HasColumnType("DECIMAL(5,2)");
            pr.Property(p => p.Name).IsRequired(false).IsUnicode().HasMaxLength(200);
        });

    }
}