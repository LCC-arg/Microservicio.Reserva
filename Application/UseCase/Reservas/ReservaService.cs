using Application.Interfaces;
using Application.Request;
using Application.Response;
using Domain.Entities;

namespace Application.UseCase.Reservas
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaCommand _command;
        private readonly IReservaQuery _query;

        public ReservaService(IReservaCommand command, IReservaQuery query)
        {
            _command = command;
            _query = query;
        }

        public Reserva GetReservaById(int reservaId)
        {
            return _query.GetReservaById(reservaId);
        }

        public List<Reserva> GetReservaList()
        {
            return _query.GetReservaList();
        }


        public List<ReservaResponse> GetReservaListFilters(string fecha, string clase, string orden)
        {
            var reservaList = _query.GetReservaListFilters(fecha, clase, orden);

            List<ReservaResponse> reservaResponseList = new List<ReservaResponse>();

            foreach (var reserva in reservaList)
            {
                var reservaResponse = new ReservaResponse
                {
                    Id = reserva.ReservaId,
                    Fecha = reserva.Fecha,
                    Precio = reserva.Precio,
                    Asiento = reserva.NumeroAsiento,
                    Clase = reserva.Clase,
                };
                reservaResponseList.Add(reservaResponse);
            }

            return reservaResponseList;
        }

        public ReservaResponse CreateReserva(ReservaRequest request)
        {
            var reserva = new Reserva
            {
                Fecha = DateTime.Now.Date,
                Precio = request.Precio,
                NumeroAsiento = request.NumeroAsiento,
                Clase = request.Clase,
            };

            _command.InsertReserva(reserva);

            return new ReservaResponse
            {
                Id = reserva.ReservaId,
                Fecha = reserva.Fecha,
                Precio = reserva.Precio,
                Asiento = reserva.NumeroAsiento,
                Clase = reserva.Clase,
            };
        }

        public ReservaResponse RemoveReserva(int reservaId)
        {
            if (_query.GetReservaById(reservaId) == null)
            {
                throw new ArgumentException($"No se encontró la reserva que desea eliminar con el identificador '{reservaId}'.");
            }

            var reserva = _command.RemoveReserva(reservaId);

            return new ReservaResponse
            {
                Id = reserva.ReservaId,
                Fecha = reserva.Fecha,
                Precio = reserva.Precio,
                Asiento = reserva.NumeroAsiento,
                Clase = reserva.Clase,
            };
        }

        public ReservaResponse UpdateReserva(int reservaId, ReservaRequest request)
        {
            var reserva = _query.GetReservaById(reservaId);

            if (reserva == null)
            {
                throw new ArgumentException($"No se encontró la reserva con el identificador {reservaId}.");
            }

            reserva.NumeroAsiento = request.NumeroAsiento;
            reserva.Clase = request.Clase;
            reserva.Precio = request.Precio;

            _command.UpdateReserva(reserva);

            return new ReservaResponse
            {
                Id = reserva.ReservaId,
                Fecha = reserva.Fecha,
                Precio = reserva.Precio,
                Asiento = reserva.NumeroAsiento,
                Clase = reserva.Clase,
            };
        }
    }
}
