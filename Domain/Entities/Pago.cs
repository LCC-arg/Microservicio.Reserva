namespace Domain.Entities
{
    public class Pago
    {
        public int PagoId { get; set; }
        public DateTime Fecha { get; set; }
        public int Monto { get; set; }

        public int ReservaId { get; set; }
        public Reserva Reserva { get; set; }

        public Factura Factura { get; set; }

        public int MetodoPagoId { get; set; }
        public MetodoPago MetodoPago { get; set; }
    }
}
