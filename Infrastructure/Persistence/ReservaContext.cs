using Domain.Entities;
using Infrastructure.Config;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ReservaContext : DbContext
    {
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<MetodoPago> MetodoPagos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public ReservaContext(DbContextOptions<ReservaContext> options)
        : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ReservaConfig());
            //modelBuilder.ApplyConfiguration(new ReservaData());

            modelBuilder.ApplyConfiguration(new PagoConfig());
            //modelBuilder.ApplyConfiguration(new PagoData());

            modelBuilder.ApplyConfiguration(new MetodoPagoConfig());
            modelBuilder.ApplyConfiguration(new MetodoPagoData());

            modelBuilder.ApplyConfiguration(new FacturaConfig());
            //modelBuilder.ApplyConfiguration(new FacturaData());
        }
    }
}
