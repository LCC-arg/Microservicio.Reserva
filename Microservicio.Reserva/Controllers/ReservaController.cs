using Application.Interfaces;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace Microservicio.Reserva.Controllers
{
    [Route("api/[controller]")]
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
        public IActionResult GetReservaListFilters(string? fecha, string? clase, string? orden = "ASC")
        {
            var result = _service.GetReservaListFilters(fecha, clase, orden);
            return new JsonResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ReservaResponse), 201)]
        public IActionResult CreateReserva(ReservaRequest request)
        {
            try
            {
                var result = _service.CreateReserva(request);
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch(Exception ex)
            {
                return Unauthorized(new BadRequest
                {
                    Message = ex.Message
                });
            }

        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReservaResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        public IActionResult GetReservaById(int id)
        {
            try
            {
                var result = _service.GetReservaById(id);
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
        [ProducesResponseType(typeof(ReservaResponse), 200)]
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
        [ProducesResponseType(typeof(BadRequest), 404)]
        public IActionResult RemoveReserva(int id)
        {
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
