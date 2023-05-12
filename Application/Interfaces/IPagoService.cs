using Application.Request;
using Application.Response;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPagoService
    {
        PagoResponse GetPagoById(int pagoId);
        List<Pago> GetPagoList();
        List<PagoResponse> GetPagoListFilters(int metodoPago, string fecha, int monto, string orden);
        PagoResponse CreatePago(PagoRequest pago);
        Pago RemovePago(int pagoId);
        Pago UpdatePago(int pagoId);
        bool ExisteReservaPagada(int reservaId);
    }
}
