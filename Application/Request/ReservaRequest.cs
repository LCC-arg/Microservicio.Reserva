namespace Application.Request
{
    public class ReservaRequest
    {
        public int NumeroAsiento { get; set; }
        public string Clase { get; set; }
        public int Precio { get; set; }
        public int ViajeId { get; set; }
        public string Token { get; set; }
    }
}
