using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class ReservaGetResponse
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Precio { get; set; }
        public int Asiento { get; set; }
        public string Clase { get; set; }
        public UsuarioResponse Usuario { get; set; }
    }
}
