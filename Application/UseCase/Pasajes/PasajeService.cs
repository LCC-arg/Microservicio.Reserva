using Application.Interfaces;
using Application.Request;
using Application.Response;
using Domain.Entities;

namespace Application.UseCase.Pasajes
{
    public class PasajeService : IPasajeService
    {
        private readonly IPasajeCommand _command;
        private readonly IPasajeQuery _query;

        private readonly IReservaQuery _reservaQuery;

        public PasajeService(IPasajeCommand command, IPasajeQuery query, IReservaQuery reservaQuery)
        {
            _command = command;
            _query = query;
            _reservaQuery = reservaQuery;
        }

        public PasajeResponse GetPasajeById(int pasajeId)
        {
            var pasaje = _query.GetPasajeById(pasajeId);

            if (pasaje == null)
            {
                throw new ArgumentException($"No se encontró el pasaje con el identificador {pasajeId}.");
            }

            return new PasajeResponse
            {
                Id = pasaje.PasajeId,
                Nota = pasaje.Nota,
                Reserva = new ReservaResponse
                {
                    Id = pasaje.Reserva.ReservaId,
                    Fecha = pasaje.Reserva.Fecha,
                    Precio = pasaje.Reserva.Precio,
                    Asiento = pasaje.Reserva.NumeroAsiento,
                    Clase = pasaje.Reserva.Clase,
                },
            };
        }

        public List<Pasaje> GetPasajeList()
        {
            return _query.GetPasajeList();
        }

        public PasajeResponse CreatePasaje(PasajeRequest request)
        {
            var reserva = _reservaQuery.GetReservaById(request.Reserva);

            if (reserva == null)
            {
                throw new ArgumentException($"No se encontró la reserva con el identificador {request.Reserva}.");
            }

            var pasaje = new Pasaje
            {
                Nota = request.Nota,
                ReservaId = request.Reserva,
                Reserva = reserva,
            };

            _command.InsertPasaje(pasaje);

            return new PasajeResponse
            {
                Id = pasaje.PasajeId,
                Nota = pasaje.Nota,
                Reserva = new ReservaResponse
                {
                    Id = reserva.ReservaId,
                    Fecha = reserva.Fecha,
                    Precio = reserva.Precio,
                    Asiento = reserva.NumeroAsiento,
                    Clase = reserva.Clase,
                },
            };
        }

        public PasajeResponse RemovePasaje(int pasajeId)
        {
            if (_query.GetPasajeById(pasajeId) == null)
            {
                throw new ArgumentException($"No se encontró el pasaje que desea eliminar con el identificador '{pasajeId}'.");
            }

            var pasaje = _command.RemovePasaje(pasajeId);

            return new PasajeResponse
            {
                Id = pasaje.PasajeId,
                Nota = pasaje.Nota,
                Reserva = new ReservaResponse
                {
                    Id = pasaje.Reserva.ReservaId,
                    Fecha = pasaje.Reserva.Fecha,
                    Precio = pasaje.Reserva.Precio,
                    Asiento = pasaje.Reserva.NumeroAsiento,
                    Clase = pasaje.Reserva.Clase,
                },
            };
        }

        public PasajeResponse UpdatePasaje(int pasajeId, PasajeRequest request)
        {
            var pasaje = _query.GetPasajeById(pasajeId);

            var reserva = _reservaQuery.GetReservaById(request.Reserva);

            if (pasaje == null)
            {
                throw new ArgumentException($"No se encontró el pasaje con el identificador {pasajeId}.");
            }

            if (reserva == null)
            {
                throw new ArgumentException($"No se encontró la reserva con el identificador {request.Reserva}.");
            }

            pasaje.Nota = request.Nota;
            pasaje.ReservaId = request.Reserva;
            pasaje.Reserva = reserva;


            _command.UpdatePasaje(pasaje);

            return new PasajeResponse
            {
                Id = pasaje.PasajeId,
                Nota = pasaje.Nota,
                Reserva = new ReservaResponse
                {
                    Id = reserva.ReservaId,
                    Fecha = reserva.Fecha,
                    Precio = reserva.Precio,
                    Asiento = reserva.NumeroAsiento,
                    Clase = reserva.Clase,
                },
            };
        }
    }
}
