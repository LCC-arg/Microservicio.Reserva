using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config
{
    public class ReservaConfig : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> entityBuilder)
        {
            entityBuilder.ToTable("Reserva");
            entityBuilder.HasKey(e => e.ReservaId);

            entityBuilder.HasOne<Pago>(m => m.Pago)
            .WithOne(cm => cm.Reserva)
            .HasForeignKey<Pago>(cm => cm.ReservaId);

            entityBuilder.HasMany(m => m.ReservaPasajeros)
            .WithOne(cm => cm.Reserva)
            .HasForeignKey(cm => cm.ReservaId);
        }
    }
}
