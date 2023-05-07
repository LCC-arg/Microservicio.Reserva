using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

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
            var factura = _context.Facturas.FirstOrDefault(x => x.FacturaId == facturaId);

            return factura;
        }

        public List<Factura> GetFacturaList()
        {
            var facturaList = _context.Facturas.ToList();

            return facturaList;
        }
    }
}
