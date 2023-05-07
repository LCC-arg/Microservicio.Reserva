namespace Domain.Entities
{
    public class MetodoPago
    {
        public int MetodoPagoId { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Pago> Pagos { get; set; }
    }
}
