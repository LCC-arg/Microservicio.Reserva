namespace Application.Request
{
    public class PagoRequest
    {
        public List<int> Reservas { get; set; }
        public int MetodoPago { set; get; }
        public string NumeroTarjeta { get; set; }
    }
}
