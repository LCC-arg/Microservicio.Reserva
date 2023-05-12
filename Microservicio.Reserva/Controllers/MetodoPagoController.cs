using Application.Interfaces;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace Microservicio.Reserva.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetodoPagoController : ControllerBase
    {
        private readonly IMetodoPagoService _service;

        public MetodoPagoController(IMetodoPagoService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(MetodoPagoResponse), 200)]
        public IActionResult GetMetodoPagoList()
        {
            var result = _service.GetMetodoPagoList();
            return new JsonResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(MetodoPagoResponse), 201)]
        [ProducesResponseType(typeof(BadRequest), 409)]
        public IActionResult CreateMetodoPago(MetodoPagoRequest request)
        {
            if (_service.ExisteMetodoPagoDescripcion(request.Descripcion))
            {
                return Conflict(new BadRequest
                {
                    Message = $"Ya existe un metodo de pago con esa descripcion '{request.Descripcion}'."
                });
            }

            var result = _service.CreateMetodoPago(request);
            return new JsonResult(result) { StatusCode = 201 };
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MetodoPagoResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        public IActionResult GetMetodoPagoById(int id)
        {
            try
            {
                var result = _service.GetMetodoPagoById(id);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return NotFound(new BadRequest
                {
                    Message = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(MetodoPagoResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        [ProducesResponseType(typeof(BadRequest), 409)]
        public IActionResult UpdateMetodoPago(int id, MetodoPagoRequest request)
        {
            if (_service.ExisteMetodoPagoDescripcion(request.Descripcion))
            {
                return Conflict(new BadRequest
                {
                    Message = $"Ya existe un metodo de pago con esa descripcion '{request.Descripcion}'."
                });
            }
            try
            {
                var result = _service.UpdateMetodoPago(id, request);
                return new JsonResult(result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new BadRequest
                {
                    Message = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(MetodoPagoResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        public IActionResult RemoveMetodoPago(int id)
        {
            try
            {
                var result = _service.RemoveMetodoPago(id);
                return new JsonResult(result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new BadRequest
                {
                    Message = ex.Message
                });
            }
        }
    }
}
