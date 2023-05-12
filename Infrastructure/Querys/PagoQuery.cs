using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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
            var pago = _context.Pagos
                .Include(s => s.MetodoPago)
                .Include(s => s.Reserva)
                .FirstOrDefault(x => x.PagoId == pagoId);

            return pago;
        }

        public List<Pago> GetPagoList()
        {
            var pagoList = _context.Pagos.ToList();

            return pagoList;
        }

        public List<Pago> GetPagoListFilters(int metodoPago, string fecha, int monto, string orden)
        {
            var pagoList = _context.Pagos
                .Include(s => s.MetodoPago)
                .Include(s => s.Reserva)
                .OrderBy(p => p.Monto)
                .ToList();

            if (metodoPago != 0)
            {
                pagoList = pagoList.Where(p => p.MetodoPagoId == metodoPago).ToList();
            }

            if (fecha != null)
            {
                DateTime dateTime = DateTime.Parse(fecha);
                pagoList = pagoList.Where(p => p.Fecha == dateTime).ToList();
            }

            if (monto != 0)
            {
                pagoList = pagoList.Where(p => p.Monto == monto).ToList();
            }

            if (orden.ToLower() == "desc")
            {
                pagoList = pagoList.OrderByDescending(p => p.Monto).ToList();
            }

            return pagoList;
        }
        public bool ExisteReservaPagada(int reservaId)
        {
            bool existeReservaPagada = _context.Pagos
                .Any(cm => cm.ReservaId == reservaId);

            if (existeReservaPagada)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
