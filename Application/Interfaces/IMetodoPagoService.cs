using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMetodoPagoService
    {
        MetodoPago GetMetodoPagoById(int metodoPagoId);
        List<MetodoPago> GetMetodoPagoList();
        MetodoPago CreateMetodoPago(MetodoPago metodoPago);
        MetodoPago RemoveMetodoPago(int metodoPagoId);
        MetodoPago UpdateMetodoPago(int metodoPagoId);
    }
}
