using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Command
{
    public class PasajeCommand : IPasajeCommand
    {
        private readonly ReservaContext _context;

        public PasajeCommand(ReservaContext context)
        {
            _context = context;
        }

        public Pasaje InsertPasaje(Pasaje pasaje)
        {
            _context.Add(pasaje);
            _context.SaveChanges();

            return pasaje;
        }

        public Pasaje RemovePasaje(int pasajeId)
        {
            var pasaje = _context.Pasajes
                .FirstOrDefault(x => x.PasajeId == pasajeId);

            _context.Remove(pasaje);
            _context.SaveChanges();

            return pasaje;
        }

        public Pasaje UpdatePasaje(Pasaje pasaje)
        {
            _context.Update(pasaje);
            _context.SaveChanges();

            return pasaje;
        }

    }
}
