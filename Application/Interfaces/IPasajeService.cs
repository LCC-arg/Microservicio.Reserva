using Application.Request;
using Application.Response;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPasajeService
    {
        PasajeResponse GetPasajeById(int pasajeId);
        List<Pasaje> GetPasajeList();
        PasajeResponse CreatePasaje(PasajeRequest request);
        PasajeResponse RemovePasaje(int pasajeId);
        PasajeResponse UpdatePasaje(int pasajeId, PasajeRequest request);
    }
}
