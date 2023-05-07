using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Querys
{
    public class PagoQuery : IPagoQuery
    {
        private readonly ReservaContext _context;

        public PagoQuery(ReservaContext context)
        {
            _context = context;
        }

        public Pago GetPagoById(int pagoId)
        {
            var pago = _context.Pagos.FirstOrDefault(x => x.PagoId == pagoId);

            return pago;
        }

        public List<Pago> GetPagoList()
        {
            var pagoList = _context.Pagos.ToList();

            return pagoList;
        }
    }
}
