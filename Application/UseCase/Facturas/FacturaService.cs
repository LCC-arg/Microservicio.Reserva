using Application.Interfaces;
using Application.Response;
using Application.UserServices;
using Domain.Entities;
using Newtonsoft.Json.Linq;

namespace Application.UseCase.Facturas
{
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaCommand _command;
        private readonly IFacturaQuery _query;

        private readonly IUserServiceViaje _userServiceViaje;

        public FacturaService(IFacturaCommand command, IFacturaQuery query, IUserServiceViaje userServiceViaje)
        {
            _command = command;
            _query = query;
            _userServiceViaje = userServiceViaje;
        }

        public FacturaResponse GetFacturaById(int facturaId)
        {
            var factura = _query.GetFacturaById(facturaId);

            if (factura == null)
            {
                throw new ArgumentException($"No se encontró la factura con el identificador {facturaId}.");
            }

            return MappingFactura(factura);
        }

        public List<FacturaResponse> GetFacturaList()
        {
            var facturaList = _query.GetFacturaList();

            List<FacturaResponse> facturaResponseList = new List<FacturaResponse>();

            foreach (var factura in facturaList)
            {
                facturaResponseList.Add(MappingFactura(factura));
            }

            return facturaResponseList;
        }

        private  FacturaResponse MappingFactura(Factura factura)
        {
            var viajeCompleto = _userServiceViaje.ObtenerViaje(factura.Pago.Reserva.ViajeId);
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
                Id = factura.Pago.Reserva.ViajeId,
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
            return new FacturaResponse
            {
                Id = factura.FacturaId,
                Estado = factura.Estado,
                Fecha = factura.Fecha,
                Monto = factura.Monto,
                Pago = new PagoResponse
                {
                    Id = factura.Pago.PagoId,
                    Fecha = factura.Pago.Fecha,
                    Monto = factura.Pago.Monto,
                    NumeroTarjeta = factura.Pago.NumeroTarjeta,

                    Reserva = new ReservaResponse
                    {
                        Id = factura.Pago.Reserva.ReservaId,
                        Fecha = factura.Pago.Reserva.Fecha,
                        Precio = factura.Pago.Reserva.Precio,
                        Asiento = factura.Pago.Reserva.NumeroAsiento,
                        Clase = factura.Pago.Reserva.Clase,
                        Pasajero = factura.Pago.Reserva.PasajeroId,
                        Viaje = viajeCompletoResponse,
                        Usuario = factura.Pago.Reserva.UsuarioId,
                    },

                    MetodoPago = new MetodoPagoResponse
                    {
                        Id = factura.Pago.MetodoPago.MetodoPagoId,
                        Descripcion = factura.Pago.MetodoPago.Descripcion,
                    }
                }
            };
        }
    }
}
