using Application.Interfaces;
using Domain.Entities;

namespace Application.UseCase.Pasajes
{
    public class PasajeService : IPasajeService
    {
        private readonly IPasajeCommand _command;
        private readonly IPasajeQuery _query;

        public PasajeService(IPasajeCommand command, IPasajeQuery query)
        {
            _command = command;
            _query = query;
        }

        public Pasaje GetPasajeById(int pasajeId)
        {
            return _query.GetPasajeById(pasajeId);
        }

        public List<Pasaje> GetPasajeList()
        {
            return _query.GetPasajeList();
        }

        public Pasaje CreatePasaje(Pasaje pasaje)
        {
            return _command.InsertPasaje(pasaje);
        }

        public Pasaje RemovePasaje(int pasajeId)
        {
            return _command.RemovePasaje(pasajeId);
        }

        public Pasaje UpdatePasaje(int pasajeId)
        {
            var pasaje = _query.GetPasajeById(pasajeId);

            return _command.UpdatePasaje(pasaje);
        }
    }
}
