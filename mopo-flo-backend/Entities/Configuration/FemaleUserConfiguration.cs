using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace violet.backend.Entities.Configuration;

public class FemaleUserConfiguration : IEntityTypeConfiguration<FemaleUser>
{
    public void Configure(EntityTypeBuilder<FemaleUser> builder)
    {
        builder.ToTable("FemaleUsers")
            .UseTptMappingStrategy();

        builder.OwnsOne(x => x.FemaleProfile, pr =>
        {
            pr.Property(p => p.Name).IsRequired(false).IsUnicode().HasMaxLength(200);
        });

        builder.OwnsMany(x => x.Periods);
    }
}