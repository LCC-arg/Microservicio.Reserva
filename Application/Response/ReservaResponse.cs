namespace Application.Response
{
    public class ReservaResponse
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Precio { get; set; }
        public int Asiento { get; set; }
        public string Clase { get; set; }
        public Guid Usuario { get; set; }
        public int Viaje { get; set; }
        public int Pasajero { get; set; }
    }
}
