using Domain.Catalogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration
{   
    public class CatalogTypeEntityTypeConfiguration : IEntityTypeConfiguration<CatalogType>
    {
        public void Configure(EntityTypeBuilder<CatalogType> builder)
        {
            builder
                .Property(x => x.Type)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .HasOne(x => x.ParentCatalogType)
                .WithMany(x => x.SubType)
                .HasForeignKey(x => x.ParentCatalogTypeId);
        }
    }
}
