using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class PagoGetResponse
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Monto { get; set; }

        public ReservaFacturaResponse Reserva { get; set; }
        public MetodoPagoResponse MetodoPago { get; set; }
    }
}
