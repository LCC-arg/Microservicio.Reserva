using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class PagoData : IEntityTypeConfiguration<Pago>
    {
        public void Configure(EntityTypeBuilder<Pago> entityBuilder)
        {
            entityBuilder.HasData
            (
                new Pago
                {
                    PagoId = 1,
                    Fecha = DateTime.Now.Date,
                    Monto = 2000,
                    ReservaId = 1,
                    MetodoPagoId = 1,
                    NumeroTarjeta = "4456 4567 4345 2334"
                }
            );
        }
    }
}
