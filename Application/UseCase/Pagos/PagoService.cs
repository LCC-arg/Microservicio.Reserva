using Application.Interfaces;
using Application.Request;
using Application.Response;
using Application.UserServices;
using Domain.Entities;
using Newtonsoft.Json.Linq;

namespace Application.UseCase.Pagos
{
    public class PagoService : IPagoService
    {
        private readonly IPagoCommand _command;
        private readonly IPagoQuery _query;

        private readonly IMetodoPagoQuery _metodoPagoQuery;
        private readonly IReservaQuery _reservaQuery;

        private readonly IUserServiceViaje _userServiceViaje;

        public PagoService(IPagoCommand command, IPagoQuery query, IMetodoPagoQuery metodoPagoQuery, IReservaQuery reservaQuery, IUserServiceViaje userServiceViaje)
        {
            _command = command;
            _query = query;
            _metodoPagoQuery = metodoPagoQuery;
            _reservaQuery = reservaQuery;
            _userServiceViaje = userServiceViaje;
        }

        public PagoResponse GetPagoById(int pagoId)
        {
            var pago = _query.GetPagoById(pagoId);

            if (pago == null)
            {
                throw new ArgumentException($"No se encontró el pago con el identificador {pagoId}.");
            }

            return MappingPago(pago);
        }

        public List<PagoResponse> GetPagoListFilters(int metodoPago, string fecha, int monto, string orden)
        {
            var pagoList = _query.GetPagoListFilters(metodoPago, fecha, monto, orden);

            List<PagoResponse> pagoResponseList = new List<PagoResponse>();

            foreach (var pago in pagoList)
            {
                pagoResponseList.Add(MappingPago(pago));
            }

            return pagoResponseList;
        }

        public List<PagoResponse> CreatePago(PagoRequest request)
        {
            List<Reserva> reservaList = new List<Reserva>();

            List<Pago> pagoList = new List<Pago>();

            List<PagoResponse> pagoResponseList = new List<PagoResponse>();

            foreach (var reserva in request.Reservas)
            {
                var reservaGet = _reservaQuery.GetReservaById(reserva);

                if (reservaGet == null)
                {
                    throw new ArgumentException($"No se encontró la reserva con el identificador {reserva}.");
                }

                reservaList.Add(reservaGet);
            }

            var metodoPago = _metodoPagoQuery.GetMetodoPagoById(request.MetodoPago);

            if (metodoPago == null)
            {
                throw new ArgumentException($"No se encontró el metodo de pago con el identificador {request.MetodoPago}.");
            }

            foreach (var reserva in reservaList)
            {
                var pago = new Pago
                {
                    Fecha = DateTime.Now.Date,
                    Monto = reserva.Precio,
                    ReservaId = reserva.ReservaId,
                    Reserva = reserva,
                    MetodoPagoId = metodoPago.MetodoPagoId,
                    MetodoPago = metodoPago,
                    NumeroTarjeta = request.NumeroTarjeta
                };

                var factura = new Factura
                {
                    Estado = "Paga",
                    Monto = reserva.Precio,
                    Fecha = reserva.Fecha,
                    PagoId = pago.PagoId,
                    Pago = pago
                };

                pago.Factura = factura;

                _command.InsertPago(pago);

                pagoList.Add(pago);
            }

            foreach (var pago in pagoList)
            {
                pagoResponseList.Add(MappingPago(pago));
            }

            return pagoResponseList;
        }

        public bool ExisteReservaPagada(int reservaId)
        {
            return _query.ExisteReservaPagada(reservaId);
        }

        private  PagoResponse MappingPago(Pago pago)
        {
            var viajeCompleto = _userServiceViaje.ObtenerViaje(pago.Reserva.ViajeId);
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(viajeCompleto);
            JToken token = JToken.Parse(jsonString);
            List<int> escalasResponse = new List<int>();
            var prueba = token.SelectToken("escalas");
            var cant = prueba.Count<JToken>();
            for (int i = 0; i < cant; i++)
            { escalasResponse.Add((int)prueba[i]); }

            List<int> serviciosResponse = new List<int>();
            var servicios = token.SelectToken("servicios");
            var cantServicios = servicios.Count<JToken>();
            for (int i = 0; i < cantServicios; i++)
            {
                serviciosResponse.Add((int)servicios[i]);

            }

            ViajeCompletoResponse viajeCompletoResponse = new ViajeCompletoResponse
            {
                Id = pago.Reserva.ViajeId,
                TransporteId = (int)token.SelectToken("transporteId"),
                TipoTransporte = (string)token.SelectToken("tipoTransporte"),
                Duracion = (string)token.SelectToken("duracion"),
                FechaSalida = (DateTime)token.SelectToken("fechaSalida"),
                FechaLlegada = (DateTime)token.SelectToken("fechaLlegada"),
                TipoViaje = (string)token.SelectToken("tipoViaje"),
                AsientosDisponibles = (int)token.SelectToken("asientosDisponibles"),
                Precio = (int)token.SelectToken("precio"),
                CiudadOrigen = (int)token.SelectToken("ciudadOrigen"),
                CiudadDestino = (int)token.SelectToken("ciudadDestino"),
                CiudadDestinoDescripcion = (string)token.SelectToken("ciudadDestinoDescripcion"),
                CiudadDestinoImagen = (string)token.SelectToken("ciudadDestinoImagen"),
                Escalas = escalasResponse,
                Servicios = serviciosResponse

            };

            var pasajeroCompleto = _userServiceViaje.ObtenerPasajero(pago.Reserva.PasajeroId);

            string jsonPasajero = Newtonsoft.Json.JsonConvert.SerializeObject(pasajeroCompleto);
            JToken tokenPasajero = JToken.Parse(jsonPasajero);

            PasajeroResponse pasajeroCompletoResponse = new PasajeroResponse
            {
                Id = pago.Reserva.PasajeroId,
                Nombre = (string)tokenPasajero.SelectToken("nombre"),
                Apellido = (string)tokenPasajero.SelectToken("apellido"),
                Dni = (int)tokenPasajero.SelectToken("dni")
            };

            return new PagoResponse
            {
                Id = pago.PagoId,
                Fecha = pago.Fecha,
                Monto = pago.Monto,
                NumeroTarjeta = pago.NumeroTarjeta,

                Reserva = new ReservaResponse
                {
                    Id = pago.Reserva.ReservaId,
                    Fecha = pago.Reserva.Fecha,
                    Precio = pago.Reserva.Precio,
                    Asiento = pago.Reserva.NumeroAsiento,
                    Clase = pago.Reserva.Clase,
                    Pasajero = pasajeroCompleto,
                    Viaje = viajeCompletoResponse,
                    Usuario = pago.Reserva.UsuarioId,
                },

                MetodoPago = new MetodoPagoResponse
                {
                    Id = pago.MetodoPago.MetodoPagoId,
                    Descripcion = pago.MetodoPago.Descripcion,
                }
            };
        }

        //private Guid ObtenerGuidToken(string token)
        //{
        //    var jwtHandler = new JwtSecurityTokenHandler();
        //    var decodedToken = jwtHandler.ReadJwtToken(token);

        //    IEnumerable<Claim> claims = decodedToken.Claims;

        //    string firstClaimValue = string.Empty;

        //    foreach (Claim claim in claims)
        //    {
        //        string claimType = claim.Type;
        //        string claimValue = claim.Value;

        //        if (string.IsNullOrEmpty(firstClaimValue))
        //        {
        //            firstClaimValue = claimValue;
        //        }
        //    }

        //    return Guid.Parse(firstClaimValue);
        //}
    }
}
