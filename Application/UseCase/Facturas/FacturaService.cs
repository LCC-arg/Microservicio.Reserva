using Application.Interfaces;
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

        public Factura GetFacturaById(int facturaId)
        {
            return _query.GetFacturaById(facturaId);
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
