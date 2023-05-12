using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Querys
{
    public class FacturaQuery : IFacturaQuery
    {
        private readonly ReservaContext _context;

        public FacturaQuery(ReservaContext context)
        {
            _context = context;
        }

        public Factura GetFacturaById(int facturaId)
        {
            var factura = _context.Facturas
                .Include(s => s.Pago)
                    .ThenInclude(s => s.MetodoPago)
                .Include(s => s.Pago)
                    .ThenInclude(s => s.Reserva)

                .FirstOrDefault(x => x.FacturaId == facturaId);

            return factura;
        }

        public List<Factura> GetFacturaList()
        {
            var facturaList = _context.Facturas.ToList();

            return facturaList;
        }
    }
}
