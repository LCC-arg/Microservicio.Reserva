using Application.Request;
using Application.Response;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IReservaService
    {
        Reserva GetReservaById(int reservaId);
        List<Reserva> GetReservaList();
        List<ReservaResponse> GetReservaListFilters(string fecha, string clase, string orden);
        ReservaResponse CreateReserva(ReservaRequest reserva);
        ReservaResponse RemoveReserva(int reservaId);
        ReservaResponse UpdateReserva(int reservaId, ReservaRequest request);
        bool ExisteReservaPagada(int id);
    }
}
