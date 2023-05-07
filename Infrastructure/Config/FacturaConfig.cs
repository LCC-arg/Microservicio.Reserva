using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config
{
    public class FacturaConfig : IEntityTypeConfiguration<Factura>
    {
        public void Configure(EntityTypeBuilder<Factura> entityBuilder)
        {
            entityBuilder.ToTable("Factura");
            entityBuilder.HasKey(e => e.FacturaId);

        }
    }
}
