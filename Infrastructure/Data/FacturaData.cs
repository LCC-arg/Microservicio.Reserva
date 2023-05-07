using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class FacturaData : IEntityTypeConfiguration<Factura>
    {
        public void Configure(EntityTypeBuilder<Factura> entityBuilder)
        {
            entityBuilder.HasData
            (
                new Factura
                {
                    FacturaId = 1,
                    Estado = "Paga",
                    Monto = 2000,
                    Fecha = DateTime.Now.Date,
                    PagoId = 1,
                }
            );
        }
    }
}
