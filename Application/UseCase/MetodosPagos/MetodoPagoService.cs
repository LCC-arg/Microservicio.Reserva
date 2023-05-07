using Application.Interfaces;
using Domain.Entities;

namespace Application.UseCase.MetodosPagos
{
    public class MetodoPagoService : IMetodoPagoService
    {
        private readonly IMetodoPagoCommand _command;
        private readonly IMetodoPagoQuery _query;

        public MetodoPagoService(IMetodoPagoCommand command, IMetodoPagoQuery query)
        {
            _command = command;
            _query = query;
        }

        public MetodoPago GetMetodoPagoById(int metodoPagoId)
        {
            return _query.GetMetodoPagoById(metodoPagoId);
        }

        public List<MetodoPago> GetMetodoPagoList()
        {
            return _query.GetMetodoPagoList();
        }

        public MetodoPago CreateMetodoPago(MetodoPago metodoPago)
        {
            return _command.InsertMetodoPago(metodoPago);
        }

        public MetodoPago RemoveMetodoPago(int metodoPagoId)
        {
            return _command.RemoveMetodoPago(metodoPagoId);
        }

        public MetodoPago UpdateMetodoPago(int metodoPagoId)
        {
            var metodoPago = _query.GetMetodoPagoById(metodoPagoId);

            return _command.UpdateMetodoPago(metodoPago);
        }
    }
}
