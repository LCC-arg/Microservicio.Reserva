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

                    Reserva = new ReservaResponse
                    {
                        Id = factura.Pago.Reserva.ReservaId,
                        Fecha = factura.Pago.Reserva.Fecha,
                        Precio = factura.Pago.Reserva.Precio,
                        Asiento = factura.Pago.Reserva.NumeroAsiento,
                        Clase = factura.Pago.Reserva.Clase,
                    },

                    MetodoPago = new MetodoPagoResponse
                    {
                        Id = factura.Pago.MetodoPago.MetodoPagoId,
                        Descripcion = factura.Pago.MetodoPago.Descripcion,
                    }
                }
            };
        }

        public List<Factura> GetFacturaList()
        {
            return _query.GetFacturaList();
        }

        public Factura CreateFactura(Factura factura)
        {
            return _command.InsertFactura(factura);
        }

        public Factura RemoveFactura(int facturaId)
        {
            return _command.RemoveFactura(facturaId);
        }

        public Factura UpdateFactura(int facturaId)
        {
            var factura = _query.GetFacturaById(facturaId);

            return _command.UpdateFactura(factura);
        }
    }
}
