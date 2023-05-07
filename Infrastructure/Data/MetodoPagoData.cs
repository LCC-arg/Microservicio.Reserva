using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class MetodoPagoData : IEntityTypeConfiguration<MetodoPago>
    {
        public void Configure(EntityTypeBuilder<MetodoPago> entityBuilder)
        {
            entityBuilder.HasData
            (
                new MetodoPago
                {
                    MetodoPagoId = 1,
                    Descripcion = "Tarjeta"
                },

                new MetodoPago
                {
                    MetodoPagoId = 2,
                    Descripcion = "Mercado Pago"
                }

            );
        }
    }
}
