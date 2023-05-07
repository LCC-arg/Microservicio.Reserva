using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Command
{
    public class FacturaCommand : IFacturaCommand
    {
        private readonly ReservaContext _context;

        public FacturaCommand(ReservaContext context)
        {
            _context = context;
        }

        public Factura InsertFactura(Factura factura)
        {
            _context.Add(factura);
            _context.SaveChanges();

            return factura;
        }

        public Factura RemoveFactura(int facturaId)
        {
            var factura = _context.Facturas
            .FirstOrDefault(x => x.FacturaId == facturaId);

            _context.Remove(factura);
            _context.SaveChanges();

            return factura;
        }

        public Factura UpdateFactura(Factura factura)
        {
            _context.Update(factura);
            _context.SaveChanges();

            return factura;
        }
    }
}
