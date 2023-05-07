using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Command
{
    public class MetodoPagoCommand : IMetodoPagoCommand
    {
        private readonly ReservaContext _context;

        public MetodoPagoCommand(ReservaContext context)
        {
            _context = context;
        }

        public MetodoPago InsertMetodoPago(MetodoPago metodoPago)
        {
            _context.Add(metodoPago);
            _context.SaveChanges();

            return metodoPago;
        }

        public MetodoPago RemoveMetodoPago(int metodoPagoId)
        {
            var metodoPago = _context.MetodoPagos
                .FirstOrDefault(x => x.MetodoPagoId == metodoPagoId);

            _context.Remove(metodoPago);
            _context.SaveChanges();

            return metodoPago;
        }

        public MetodoPago UpdateMetodoPago(MetodoPago metodoPago)
        {
            _context.Update(metodoPago);
            _context.SaveChanges();

            return metodoPago;
        }
    }
}
