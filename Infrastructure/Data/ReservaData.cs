using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class ReservaData : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> entityBuilder)
        {
            entityBuilder.HasData
            (
                new Reserva
                {
                    ReservaId = 1,
                    Fecha = DateTime.Now.Date,
                    Precio = 2000,
                    NumeroAsiento = 4,
                    Clase = "Alta",
                }
            );
        }
    }
}
