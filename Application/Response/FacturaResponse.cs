namespace Application.Response
{
    public class FacturaResponse
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public int Monto { get; set; }
        public DateTime Fecha { get; set; }

        public PagoResponse Pago { get; set; }
    }
}
