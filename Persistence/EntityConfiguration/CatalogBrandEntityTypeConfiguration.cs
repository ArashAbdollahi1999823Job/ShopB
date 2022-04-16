using Domain.Catalogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration
{
    public class CatalogBrandEntityTypeConfiguration:IEntityTypeConfiguration<CatalogBrand>
    {
        public void Configure(EntityTypeBuilder<CatalogBrand> builder)
        {
            builder
                .Property(x => x.Brand)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
