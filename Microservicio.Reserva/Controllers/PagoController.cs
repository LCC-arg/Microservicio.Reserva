using Application.Interfaces;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace Microservicio.Reserva.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly IPagoService _service;

        public PagoController(IPagoService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagoResponse), 200)]
        public IActionResult GetPagoListFilters(int metodoPago, string? fecha, int monto, string? orden = "ASC")
        {
            var result = _service.GetPagoListFilters(metodoPago, fecha, monto, orden);
            return new JsonResult(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PagoResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        public IActionResult GetPagoById(int id)
        {
            try
            {
                var result = _service.GetPagoById(id);
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

        [HttpPost]
        [ProducesResponseType(typeof(PagoResponse), 201)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        public IActionResult CreatePago(PagoRequest request)
        {
            try
            {
                var result = _service.CreatePago(request);
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (Exception ex)
            {
                return NotFound(new BadRequest
                {
                    Message = ex.Message
                });
            }
        }
    }
}
