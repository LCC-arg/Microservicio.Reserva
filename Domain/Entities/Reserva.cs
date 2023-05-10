namespace Domain.Entities
{
    public class Reserva
    {
        public int ReservaId { get; set; }
        public DateTime Fecha { get; set; }
        public int Precio { get; set; }
        public int NumeroAsiento { get; set; }
        public string Clase { get; set; }

        public int PasajeroId { get; set; }

        public Guid UsuarioId { get; set; }

        public int ViajeId { get; set; }

        public Pago Pago { get; set; }

        public ICollection<Pasaje> Pasajes { get; set; }
    }
}
