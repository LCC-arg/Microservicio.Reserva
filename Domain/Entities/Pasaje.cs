
namespace Domain.Entities
{
    public class Pasaje
    {
        public int PasajeId { get; set; }
        public string Nota { get; set; }

        public int ReservaId { get; set; }
        public Reserva Reserva { get; set; }
    }
}
