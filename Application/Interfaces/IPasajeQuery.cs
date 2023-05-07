using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPasajeQuery
    {
        Pasaje GetPasajeById(int pasajeId);
        List<Pasaje> GetPasajeList();
    }
}
