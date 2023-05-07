using Domain.Entities;

namespace Application.Interfaces
{
    public interface IReservaQuery
    {
        Reserva GetReservaById(int reservaId);
        List<Reserva> GetReservaList();
        List<Reserva> GetReservaListFilters(string fecha, string clase, string orden);
        bool ExisteReservaPagada(int id);
    }
}
