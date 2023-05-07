using Domain.Entities;

namespace Application.Interfaces
{
    public interface IFacturaCommand
    {
        Factura InsertFactura(Factura factura);
        Factura RemoveFactura(int facturaId);
        Factura UpdateFactura(Factura factura);
    }
}
