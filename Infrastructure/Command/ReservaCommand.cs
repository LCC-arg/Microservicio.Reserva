using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Command
{
    public class ReservaCommand : IReservaCommand
    {
        private readonly ReservaContext _context;

        public ReservaCommand(ReservaContext context)
        {
            _context = context;
        }

        public Reserva InsertReserva(Reserva reserva)
        {
            _context.Add(reserva);
            _context.SaveChanges();

            return reserva;
        }

        public Reserva RemoveReserva(int reservaId)
        {
            var reserva = _context.Reservas
                .FirstOrDefault(x => x.ReservaId == reservaId);

            _context.Remove(reserva);
            _context.SaveChanges();

            return reserva;
        }

        public Reserva UpdateReserva(Reserva reserva)
        {
            _context.Update(reserva);
            _context.SaveChanges();

            return reserva;
        }
    }
}
