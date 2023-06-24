using Application.Interfaces;
using Application.Request;
using Application.Response;
using Domain.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.UseCase.Reservas
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaCommand _command;
        private readonly IReservaQuery _query;

        private readonly IUserServiceViaje _userServiceViaje;

        public ReservaService(IReservaCommand command, IReservaQuery query, IUserServiceViaje userServiceViaje)
        {
            _command = command;
            _query = query;
            _userServiceViaje = userServiceViaje;
        }

        public ReservaResponse GetReservaById(int reservaId)
        {
            var reserva = _query.GetReservaById(reservaId);

            if (reserva == null)
            {
                throw new ArgumentException($"No se encontró la reserva con el identificador {reservaId}.");
            }

            return MappingReserva(reserva);
        }

        public List<ReservaResponse> GetReservaListFilters(string fecha, string clase, string orden, Guid usuarioId)
        {
            var reservaList = _query.GetReservaListFilters(fecha, clase, orden, usuarioId);

            List<ReservaResponse> reservaResponseList = new List<ReservaResponse>();

            foreach (var reserva in reservaList)
            {
                reservaResponseList.Add(MappingReserva(reserva));
            }

            return reservaResponseList;
        }

        //public List<ReservaResponse> CreateReserva(ReservaRequest request, string token)
        //{
        //    List<Reserva> reservaList = new List<Reserva>();

        //    List<ReservaResponse> reservaResponseList = new List<ReservaResponse>();

        //    for (int i = 0; i < request.NumeroAsiento.Count; i++)
        //    {
        //        int pasajero = request.Pasajeros[i];
        //        int asientoAsignado = request.NumeroAsiento[i];

        //        var reserva = new Reserva
        //        {
        //            Fecha = DateTime.Now.Date,
        //            Precio = request.Precio,
        //            NumeroAsiento = asientoAsignado,
        //            Clase = request.Clase,
        //            ViajeId = request.ViajeId,
        //            PasajeroId = pasajero,
        //            UsuarioId = this.ObtenerGuidToken(token)
        //        };

        //        reservaList.Add(reserva);
        //        _command.InsertReserva(reserva);
        //    }

        //    _userServiceViaje.ModificarViaje(request.ViajeId, request.NumeroAsiento.Count);

        //    foreach (var reserva in reservaList)
        //    {
        //        reservaResponseList.Add(MappingReserva(reserva));
        //    }

        //    return reservaResponseList;
        //}

        public List<ReservaResponse> CreateReserva(ReservaRequest request, string token)
        {
            Random random = new Random();

            List<Reserva> reservaList = new List<Reserva>();

            List<ReservaResponse> reservaResponseList = new List<ReservaResponse>();

            foreach(var pasajero in request.Pasajeros)
            {
                var reserva = new Reserva
                {
                    Fecha = DateTime.Now.Date,
                    Precio = request.Precio,
                    NumeroAsiento = random.Next(1, 21),
                    Clase = request.Clase,
                    ViajeId = request.ViajeId,
                    PasajeroId = pasajero,
                    UsuarioId = this.ObtenerGuidToken(token)
                };

                reservaList.Add(reserva);
                _command.InsertReserva(reserva);

            }

            _userServiceViaje.ModificarViaje(request.ViajeId, request.Pasajeros.Count);

            foreach (var reserva in reservaList)
            {
                reservaResponseList.Add(MappingReserva(reserva));
            }

            return reservaResponseList;
        }

        public ReservaResponse RemoveReserva(int reservaId)
        {
            if (_query.GetReservaById(reservaId) == null)
            {
                throw new ArgumentException($"No se encontró la reserva que desea eliminar con el identificador '{reservaId}'.");
            }

            var reserva = _command.RemoveReserva(reservaId);

            return MappingReserva(reserva);
        }

        public ReservaResponse UpdateReserva(int reservaId, ReservaGetRequest request)
        {
            var reserva = _query.GetReservaById(reservaId);

            if (reserva == null)
            {
                throw new ArgumentException($"No se encontró la reserva con el identificador {reservaId}.");
            }

            reserva.NumeroAsiento = request.NumeroAsiento;
            reserva.Clase = request.Clase;
            reserva.Precio = request.Precio;
            reserva.ViajeId = request.ViajeId;
            reserva.PasajeroId = request.PasajeroId;

            _command.UpdateReserva(reserva);

            return MappingReserva(reserva);
        }

        private Guid ObtenerGuidToken(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var decodedToken = jwtHandler.ReadJwtToken(token);

            IEnumerable<Claim> claims = decodedToken.Claims;

            string firstClaimValue = string.Empty;

            foreach (Claim claim in claims)
            {
                string claimType = claim.Type;
                string claimValue = claim.Value;

                if (string.IsNullOrEmpty(firstClaimValue))
                {
                    firstClaimValue = claimValue;
                }
            }

            return Guid.Parse(firstClaimValue);
        }

        private static ReservaResponse MappingReserva(Reserva reserva)
        {
            return new ReservaResponse
            {
                Id = reserva.ReservaId,
                Fecha = reserva.Fecha,
                Precio = reserva.Precio,
                Asiento = reserva.NumeroAsiento,
                Clase = reserva.Clase,
                Pasajero = reserva.PasajeroId,
                Viaje = reserva.ViajeId,
                Usuario = reserva.UsuarioId
            };
        }
    }
}
