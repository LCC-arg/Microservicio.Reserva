using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Command
{
    public class PagoCommand : IPagoCommand
    {
        private readonly ReservaContext _context;

        public PagoCommand(ReservaContext context)
        {
            _context = context;
        }

        public Pago InsertPago(Pago pago)
        {
            _context.Add(pago);
            _context.SaveChanges();

            return pago;
        }

        public Pago RemovePago(int pagoId)
        {
            var pago = _context.Pagos
            .FirstOrDefault(x => x.PagoId == pagoId);

            _context.Remove(pago);
            _context.SaveChanges();

            return pago;
        }

        public Pago UpdatePago(Pago pago)
        {
            _context.Update(pago);
            _context.SaveChanges();

            return pago;
        }
    }
}
