namespace Domain.Entities
{
    public class Factura
    {
        public int FacturaId { get; set; }
        public string Estado { get; set; }
        public int Monto { get; set; }
        public DateTime Fecha { get; set; }

        public int PagoId { get; set; }
        public Pago Pago { get; set; }
    }
}
