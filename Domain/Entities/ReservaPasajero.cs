using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ReservaPasajero
    {
        public Guid ReservaPasajeroId { get; set; }
        public int ReservaId { get; set; }
        public Reserva Reserva { get; set; }

        public int PasajeroId { get; set; }
  
    }
}
