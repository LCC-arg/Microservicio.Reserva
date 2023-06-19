using Application.Request;
using Application.Response;

namespace Application.Interfaces
{
    public interface IReservaService
    {
        ReservaResponse GetReservaById(int reservaId);
        List<ReservaResponse> GetReservaListFilters(string fecha, string clase, string orden, Guid usuarioId);
        List<ReservaResponse> CreateReserva(ReservaRequest reserva, string token);
        ReservaResponse RemoveReserva(int reservaId);
        ReservaResponse UpdateReserva(int reservaId, ReservaGetRequest request);
    }
}
