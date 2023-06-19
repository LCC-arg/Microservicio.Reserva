using Application.Response;

namespace Application.Interfaces
{
    public interface IFacturaService
    {
        FacturaResponse GetFacturaById(int facturaId);
        List<FacturaResponse> GetFacturaList();
    }
}
