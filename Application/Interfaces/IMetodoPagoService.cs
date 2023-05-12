using Application.Request;
using Application.Response;

namespace Application.Interfaces
{
    public interface IMetodoPagoService
    {
        MetodoPagoResponse GetMetodoPagoById(int metodoPagoId);
        List<MetodoPagoResponse> GetMetodoPagoList();
        MetodoPagoResponse CreateMetodoPago(MetodoPagoRequest metodoPago);
        MetodoPagoResponse RemoveMetodoPago(int metodoPagoId);
        MetodoPagoResponse UpdateMetodoPago(int metodoPagoId, MetodoPagoRequest request);
        bool ExisteMetodoPagoDescripcion(string nombre);
    }
}
