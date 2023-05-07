using Application.Interfaces;
using Domain.Entities;

namespace Application.UseCase.Pagos
{
    public class PagoService : IPagoService
    {
        private readonly IPagoCommand _command;
        private readonly IPagoQuery _query;

        public PagoService(IPagoCommand command, IPagoQuery query)
        {
            _command = command;
            _query = query;
        }

        public Pago GetPagoById(int pagoId)
        {
            return _query.GetPagoById(pagoId);
        }

        public List<Pago> GetPagoList()
        {
            return _query.GetPagoList();
        }

        public Pago CreatePago(Pago pago)
        {
            return _command.InsertPago(pago);
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
    }
}
