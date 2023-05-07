using Application.Interfaces;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace Microservicio.Reserva.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly IReservaService _service;

        public ReservaController(IReservaService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ReservaResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        public IActionResult GetReservaListFilters(string? fecha, string? clase, string? orden = "ASC")
        {
            var result = _service.GetReservaListFilters(fecha, clase, orden);
            return new JsonResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ReservaResponse), 201)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        public IActionResult CreateReserva(ReservaRequest request)
        {
            var result = _service.CreateReserva(request);
            return new JsonResult(result) { StatusCode = 201 };
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ReservaResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        public IActionResult UpdateMercaderia(int id, ReservaRequest request)
        {
            try
            {
                var result = _service.UpdateReserva(id, request);
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
        [ProducesResponseType(typeof(ReservaResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        [ProducesResponseType(typeof(BadRequest), 409)]
        public IActionResult RemoveReserva(int id)
        {
            if (_service.ExisteReservaPagada(id))
            {
                return Conflict(new BadRequest
                {
                    Message = $"La reserva con id '{id}' que intenta borrar se encuentra pagada"
                });
            }
            try
            {
                var result = _service.RemoveReserva(id);
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
