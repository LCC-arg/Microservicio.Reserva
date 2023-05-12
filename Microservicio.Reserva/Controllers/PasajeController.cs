using Application.Interfaces;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace Microservicio.Reserva.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasajeController : ControllerBase
    {
        private readonly IPasajeService _service;

        public PasajeController(IPasajeService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PasajeResponse), 201)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        public IActionResult CreatePasaje(PasajeRequest request)
        {
            try
            {
                var result = _service.CreatePasaje(request);
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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PasajeRequest), 200)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        public IActionResult GetPasajeById(int id)
        {
            try
            {
                var result = _service.GetPasajeById(id);
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

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(PasajeRequest), 200)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        public IActionResult RemovePasaje(int id)
        {
            try
            {
                var result = _service.RemovePasaje(id);
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

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PasajeRequest), 200)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        public IActionResult UpdatePasaje(int id, PasajeRequest request)
        {
            try
            {
                var result = _service.UpdatePasaje(id, request);
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
