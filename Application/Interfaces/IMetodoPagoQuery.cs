using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMetodoPagoQuery
    {
        MetodoPago GetMetodoPagoById(int metodoPagoId);
        List<MetodoPago> GetMetodoPagoList();
        bool ExisteMetodoPagoDescripcion(string nombre);
    }
}
