using Domain.Entities;

namespace Application.Interfaces
{
    public interface IFacturaService
    {
        Factura GetFacturaById(int facturaId);
        List<Factura> GetFacturaList();
        Factura CreateFactura(Factura factura);
        Factura RemoveFactura(int facturaId);
        Factura UpdateFactura(int facturaId);
    }
}
