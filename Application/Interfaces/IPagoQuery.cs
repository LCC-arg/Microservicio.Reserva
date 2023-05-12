using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPagoQuery
    {
        Pago GetPagoById(int pagoId);
        List<Pago> GetPagoList();
        List<Pago> GetPagoListFilters(int metodoPago, string fecha, int monto, string orden);
        bool ExisteReservaPagada(int reservaId);
    }
}
