using Domain.Entities;

namespace Application.Interfaces
{
    public interface IReservaCommand
    {
        Reserva InsertReserva(Reserva reserva);
        Reserva RemoveReserva(int reservaId);
        Reserva UpdateReserva(Reserva reserva);
    }
}
