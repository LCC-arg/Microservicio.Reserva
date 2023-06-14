using Application.Interfaces;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace Microservicio.Reserva.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaService _service;

        public FacturaController(IFacturaService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(FacturaResponse), 200)]
        public IActionResult GetFacturaList()
        {
            var result = _service.GetFacturaList();
            return new JsonResult(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FacturaResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        public IActionResult GetFacturaById(int id)
        {
            try
            {
                var result = _service.GetFacturaById(id);
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
    }
}
