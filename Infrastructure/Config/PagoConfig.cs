using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config
{
    public class PagoConfig : IEntityTypeConfiguration<Pago>
    {
        public void Configure(EntityTypeBuilder<Pago> entityBuilder)
        {
            entityBuilder.ToTable("Pago");
            entityBuilder.HasKey(e => e.PagoId);

            entityBuilder.HasOne<Factura>(m => m.Factura)
            .WithOne(cm => cm.Pago)
            .HasForeignKey<Factura>(cm => cm.PagoId);
        }
    }
}
