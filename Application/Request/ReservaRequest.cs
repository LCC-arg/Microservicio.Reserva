namespace Application.Request
{
    public class ReservaRequest
    {
        //public List<int> NumeroAsiento { get; set; }
        public string Clase { get; set; }
        public int ViajeId { get; set; }
        public List<int> Pasajeros { get; set; }
    }
}
