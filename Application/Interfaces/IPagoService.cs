using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPagoService
    {
        Pago GetPagoById(int pagoId);
        List<Pago> GetPagoList();
        Pago CreatePago(Pago pago);
        Pago RemovePago(int pagoId);
        Pago UpdatePago(int pagoId);
    }
}
