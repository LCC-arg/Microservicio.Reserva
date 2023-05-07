using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPagoCommand
    {
        Pago InsertPago(Pago pago);
        Pago RemovePago(int pagoId);
        Pago UpdatePago(Pago pago);
    }
}
