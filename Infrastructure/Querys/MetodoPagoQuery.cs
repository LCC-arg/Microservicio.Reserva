using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Querys
{
    public class MetodoPagoQuery : IMetodoPagoQuery
    {
        private readonly ReservaContext _context;

        public MetodoPagoQuery(ReservaContext context)
        {
            _context = context;
        }

        public MetodoPago GetMetodoPagoById(int metodoPagoId)
        {
            var metodoPago = _context.MetodoPagos.FirstOrDefault(x => x.MetodoPagoId == metodoPagoId);

            return metodoPago;
        }

        public List<MetodoPago> GetMetodoPagoList()
        {
            var metodoPagoList = _context.MetodoPagos.ToList();

            return metodoPagoList;
        }

        public bool ExisteMetodoPagoDescripcion(string nombre)
        {
            bool existeMetodoPago = _context.MetodoPagos
                .Any(x => x.Descripcion.ToLower() == nombre.ToLower());

            if (existeMetodoPago)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
