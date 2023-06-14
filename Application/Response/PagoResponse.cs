namespace Application.Response
{
    public class PagoResponse
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Monto { get; set; }

        public string NumeroTarjeta { get; set; }

        public ReservaResponse Reserva { get; set; }
        public MetodoPagoResponse MetodoPago { get; set; }
    }
}
