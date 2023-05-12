using Application.Interfaces;
using Application.Request;
using Application.Response;
using Domain.Entities;

namespace Application.UseCase.MetodosPagos
{
    public class MetodoPagoService : IMetodoPagoService
    {
        private readonly IMetodoPagoCommand _command;
        private readonly IMetodoPagoQuery _query;

        public MetodoPagoService(IMetodoPagoCommand command, IMetodoPagoQuery query)
        {
            _command = command;
            _query = query;
        }

        public MetodoPagoResponse GetMetodoPagoById(int metodoPagoId)
        {
            var metodoPago = _query.GetMetodoPagoById(metodoPagoId);

            if (metodoPago == null)
            {
                throw new ArgumentException($"No se encontró el metodo de pago con el identificador {metodoPagoId}.");
            }

            return new MetodoPagoResponse
            {
                Id = metodoPago.MetodoPagoId,
                Descripcion = metodoPago.Descripcion,
            };
        }

        public List<MetodoPagoResponse> GetMetodoPagoList()
        {
            var metodoPagoList = _query.GetMetodoPagoList();

            List<MetodoPagoResponse> metodoPagoResponseList = new List<MetodoPagoResponse>();

            foreach (var metodoPago in metodoPagoList)
            {
                var metodoPagoResponse = new MetodoPagoResponse
                {
                    Id = metodoPago.MetodoPagoId,
                    Descripcion = metodoPago.Descripcion,
                };
                metodoPagoResponseList.Add(metodoPagoResponse);
            }

            return metodoPagoResponseList;
        }

        public MetodoPagoResponse CreateMetodoPago(MetodoPagoRequest request)
        {
            var metodoPago = new MetodoPago
            {
                Descripcion = request.Descripcion,
            };

            _command.InsertMetodoPago(metodoPago);

            return new MetodoPagoResponse
            {
                Id = metodoPago.MetodoPagoId,
                Descripcion = metodoPago.Descripcion,
            };
        }

        public MetodoPagoResponse RemoveMetodoPago(int metodoPagoId)
        {
            if (_query.GetMetodoPagoById(metodoPagoId) == null)
            {
                throw new ArgumentException($"No se encontró el metodo de pago que desea eliminar con el identificador '{metodoPagoId}'.");
            }

            var metodoPago = _command.RemoveMetodoPago(metodoPagoId);

            return new MetodoPagoResponse
            {
                Id = metodoPago.MetodoPagoId,
                Descripcion = metodoPago.Descripcion,
            };
        }

        public MetodoPagoResponse UpdateMetodoPago(int metodoPagoId, MetodoPagoRequest request)
        {
            var metodoPago = _query.GetMetodoPagoById(metodoPagoId);

            if (metodoPago == null)
            {
                throw new ArgumentException($"No se encontró el metodo de pago con el identificador {metodoPagoId}.");
            }

            metodoPago.Descripcion = request.Descripcion;

            _command.UpdateMetodoPago(metodoPago);

            return new MetodoPagoResponse
            {
                Id = metodoPago.MetodoPagoId,
                Descripcion = metodoPago.Descripcion,
            };
        }

        public bool ExisteMetodoPagoDescripcion(string nombre)
        {
            return _query.ExisteMetodoPagoDescripcion(nombre);
        }
    }
}
