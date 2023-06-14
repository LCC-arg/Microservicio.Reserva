namespace Application.Response
{
    public class ReservaFacturaResponse
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Precio { get; set; }
        public int Asiento { get; set; }
        public string Clase { get; set; }
    }
}
