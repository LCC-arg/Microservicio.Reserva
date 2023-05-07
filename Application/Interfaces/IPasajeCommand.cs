using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPasajeCommand
    {
        Pasaje InsertPasaje(Pasaje pasaje);
        Pasaje RemovePasaje(int pasajeId);
        Pasaje UpdatePasaje(Pasaje pasaje);
    }
}
