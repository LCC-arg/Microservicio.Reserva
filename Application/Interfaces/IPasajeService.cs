using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPasajeService
    {
        Pasaje GetPasajeById(int pasajeId);
        List<Pasaje> GetPasajeList();
        Pasaje CreatePasaje(Pasaje pasaje);
        Pasaje RemovePasaje(int pasajeId);
        Pasaje UpdatePasaje(int pasajeId);
    }
}
