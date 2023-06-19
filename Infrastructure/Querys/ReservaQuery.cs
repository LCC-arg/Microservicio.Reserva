using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Querys
{
    public class ReservaQuery : IReservaQuery
    {
        private readonly ReservaContext _context;

        public ReservaQuery(ReservaContext context)
        {
            _context = context;
        }

        public Reserva GetReservaById(int reservaId)
        {
            var reserva = _context.Reservas.FirstOrDefault(x => x.ReservaId == reservaId);

            return reserva;
        }

        public List<Reserva> GetReservaList()
        {
            var reservaList = _context.Reservas.ToList();

            return reservaList;
        }

        public List<Reserva> GetReservaListFilters(string fecha, string clase, string orden, Guid usuarioId)
        {
            var reservaList = _context.Reservas
                .OrderBy(p => p.Precio)
                .ToList();

            if (fecha != null)
            {
                DateTime dateTime = DateTime.Parse(fecha);
                reservaList = reservaList.Where(p => p.Fecha == dateTime).ToList();
            }

            if (clase != null)
            {
                reservaList = reservaList.Where(p => p.Clase.ToLower().Contains(clase.ToLower())).ToList();
            }

            if (orden.ToLower() == "desc")
            {
                reservaList = reservaList.OrderByDescending(p => p.Precio).ToList();
            }

            if (usuarioId != Guid.Empty)
            {
                reservaList = reservaList.Where(p => p.UsuarioId == usuarioId).ToList();
            }

            return reservaList;
        }
    }
}
