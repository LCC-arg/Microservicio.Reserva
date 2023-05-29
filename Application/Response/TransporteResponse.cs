using Domain;

namespace Application.Responses
{
    public class TransporteResponse
    {
        public int Id { get; set; }
        public CompaniaTransporteResponse CompaniaTransporte { get; set; }
        public TipoTransporteResponse TipoTransporte { get; set; }
    }
}
