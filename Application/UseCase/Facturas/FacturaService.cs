using Application.Interfaces;
using Application.Response;
using Domain.Entities;

namespace Application.UseCase.Facturas
{
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaCommand _command;
        private readonly IFacturaQuery _query;

        public FacturaService(IFacturaCommand command, IFacturaQuery query)
        {
            _command = command;
            _query = query;
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

        private static FacturaResponse MappingFactura(Factura factura)
        {
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
                        Viaje = factura.Pago.Reserva.ViajeId,
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
