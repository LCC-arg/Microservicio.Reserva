namespace Application.Response
{
    public class PasajeResponse
    {
        public int Id { get; set; }
        public string Nota { get; set; }
        public ReservaResponse Reserva { get; set; }
    }
}
