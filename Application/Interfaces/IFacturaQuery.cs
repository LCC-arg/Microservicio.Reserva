using Domain.Entities;

namespace Application.Interfaces
{
    public interface IFacturaQuery
    {
        Factura GetFacturaById(int facturaId);
        List<Factura> GetFacturaList();
    }
}
