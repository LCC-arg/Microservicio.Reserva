using Application.Interfaces;
using Application.Request;
using Application.Response;
using Application.UserServices;
using Application.UseServices;
using Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.UseCase.Pagos
{
    public class PagoService : IPagoService
    {
        private readonly IPagoCommand _command;
        private readonly IPagoQuery _query;

        private readonly IMetodoPagoQuery _metodoPagoQuery;

        private readonly IReservaQuery _reservaQuery;

        private readonly IUserServiceUsuario _userServiceUsuario;

        public PagoService(IPagoCommand command, IPagoQuery query, IMetodoPagoQuery metodoPagoQuery, IReservaQuery reservaQuery, IUserServiceUsuario userServiceUsuario)
        {
            _command = command;
            _query = query;
            _metodoPagoQuery = metodoPagoQuery;
            _reservaQuery = reservaQuery;
            _userServiceUsuario = userServiceUsuario;
        }

        public PagoResponse GetPagoById(int pagoId)
        {
            var pago = _query.GetPagoById(pagoId);

            if (pago == null)
            {
                throw new ArgumentException($"No se encontró el pago con el identificador {pagoId}.");
            }

            return new PagoResponse
            {
                Id = pago.PagoId,
                Fecha = pago.Fecha,
                Monto = pago.Monto,

                Reserva = new ReservaGetResponse
                {
                    Id = pago.Reserva.ReservaId,
                    Fecha = pago.Reserva.Fecha,
                    Precio = pago.Reserva.Precio,
                    Asiento = pago.Reserva.NumeroAsiento,
                    Clase = pago.Reserva.Clase,
                },

                MetodoPago = new MetodoPagoResponse
                {
                    Id = pago.MetodoPago.MetodoPagoId,
                    Descripcion = pago.MetodoPago.Descripcion,
                }
            };
        }

        public List<Pago> GetPagoList()
        {
            return _query.GetPagoList();
        }

        public List<PagoResponse> GetPagoListFilters(int metodoPago, string fecha, int monto, string orden)
        {
            var pagoList = _query.GetPagoListFilters(metodoPago, fecha, monto, orden);

            List<PagoResponse> pagoResponseList = new List<PagoResponse>();

            foreach (var pago in pagoList)
            {
                var pagoResponse = new PagoResponse
                {
                    Id = pago.PagoId,
                    Fecha = pago.Fecha,
                    Monto = pago.Monto,

                    Reserva = new ReservaGetResponse
                    {
                        Id = pago.Reserva.ReservaId,
                        Fecha = pago.Reserva.Fecha,
                        Precio = pago.Reserva.Precio,
                        Asiento = pago.Reserva.NumeroAsiento,
                        Clase = pago.Reserva.Clase,
                    },

                    MetodoPago = new MetodoPagoResponse
                    {
                        Id = pago.MetodoPago.MetodoPagoId,
                        Descripcion = pago.MetodoPago.Descripcion,
                    }
                };

                pagoResponseList.Add(pagoResponse);
            }

            return pagoResponseList;
        }

        public PagoResponse CreatePago(PagoRequest request)
        {
            var reserva = _reservaQuery.GetReservaById(request.Reserva);

            var metodoPago = _metodoPagoQuery.GetMetodoPagoById(request.MetodoPago);

            if (reserva == null)
            {
                throw new ArgumentException($"No se encontró la reserva con el identificador {request.Reserva}.");
            }

            if (metodoPago == null)
            {
                throw new ArgumentException($"No se encontró el metodo de pago con el identificador {request.MetodoPago}.");
            }

            var usuario = _userServiceUsuario.ObtenerUsuario(ObtenerGuidToken(request.Token), request.Token);

            var pago = new Pago
            {
                Fecha = DateTime.Now.Date,
                Monto = reserva.Precio,
                ReservaId = reserva.ReservaId,
                Reserva = reserva,
                MetodoPagoId = metodoPago.MetodoPagoId,
                MetodoPago = metodoPago,
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

            return new PagoResponse
            {
                Id = pago.PagoId,
                Fecha = pago.Fecha,
                Monto = pago.Monto,

                Reserva = new ReservaGetResponse
                {
                    Id = pago.Reserva.ReservaId,
                    Fecha = pago.Reserva.Fecha,
                    Precio = pago.Reserva.Precio,
                    Asiento = pago.Reserva.NumeroAsiento,
                    Clase = pago.Reserva.Clase,
                    Usuario = new UsuarioResponse
                    {
                        Nombre = usuario.nombre,
                        Apellido = usuario.apellido,
                        Dni = usuario.dni,
                    },
                },

                MetodoPago = new MetodoPagoResponse
                {
                    Id = pago.MetodoPago.MetodoPagoId,
                    Descripcion = pago.MetodoPago.Descripcion,
                }
            };
        }

        public Pago RemovePago(int pagoId)
        {
            return _command.RemovePago(pagoId);
        }

        public Pago UpdatePago(int pagoId)
        {
            var pago = _query.GetPagoById(pagoId);

            return _command.UpdatePago(pago);
        }

        public bool ExisteReservaPagada(int reservaId)
        {
            return _query.ExisteReservaPagada(reservaId);
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
    }
}
