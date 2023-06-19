using Application.Request;
using Application.Response;

namespace Application.Interfaces
{
    public interface IPagoService
    {
        PagoResponse GetPagoById(int pagoId);
        List<PagoResponse> GetPagoListFilters(int metodoPago, string fecha, int monto, string orden);
        List<PagoResponse> CreatePago(PagoRequest pago);
        bool ExisteReservaPagada(int reservaId);
    }
}
