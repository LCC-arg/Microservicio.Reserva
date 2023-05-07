using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMetodoPagoCommand
    {
        MetodoPago InsertMetodoPago(MetodoPago metodoPago);
        MetodoPago RemoveMetodoPago(int metodoPagoId);
        MetodoPago UpdateMetodoPago(MetodoPago metodoPago);
    }
}
