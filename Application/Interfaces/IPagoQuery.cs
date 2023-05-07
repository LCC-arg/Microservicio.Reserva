using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPagoQuery
    {
        Pago GetPagoById(int pagoId);
        List<Pago> GetPagoList();
    }
}
