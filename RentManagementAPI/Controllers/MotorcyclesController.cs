using Microsoft.AspNetCore.Mvc;
using LeaseManagement.Domain.Entities;
using LeaseManagement.Infrastructure.RabbitMQ.Publish;
using System.Text.Json;
using LeaseManagementAPI.Validations;

namespace RentManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MotorcyclesController : ControllerBase
    {
        [HttpPost]
        public IActionResult RegisterMotorcycle([FromBody] Motorcycle motorcycle)
        {
            try
            {
                APIValidator.ValidateMotorcycleRegister(motorcycle);

                string json = JsonSerializer.Serialize(motorcycle);
                GenericPublisher.PublishMessage(json, "motorcycle");

                return StatusCode(201);
            }
            catch (Exception)
            {
                return BadRequest(new { mensagem = "Dados inválidos" });
            }
        }
    }
}
