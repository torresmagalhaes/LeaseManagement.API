using LeaseManagement.Domain.Entities;
using LeaseManagement.Domain.Entities.MongoDB;
using LeaseManagement.Domain.MongoDB.Entities;
using LeaseManagement.Infrastructure.MongoDB.Implementation;
using LeaseManagement.Infrastructure.RabbitMQ.Publish;
using LeaseManagementAPI.Mappers;
using LeaseManagementAPI.Validations;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace RentManagementAPI.Controllers
{
    [ApiController]
    [Route("motos")]
    public class MotorcyclesController : ControllerBase
    {
        private readonly MotorcycleImplementation _motorcycleImplementation;

        public MotorcyclesController(MotorcycleImplementation motorcycleImplementation)
        {
            _motorcycleImplementation = motorcycleImplementation;
        }

        [HttpPost]
        public IActionResult RegisterMotorcycle([FromBody] Motorcycle motorcycle)
        {
            try
            {
                APIValidator.ValidateMotorcycleRegister(motorcycle);

                MotorcycleDocument motorcycleDocument = _motorcycleImplementation.GetByLicensePlate(motorcycle.LicensePlate);
                if(motorcycleDocument != null)
                    throw new Exception();

                
                _motorcycleImplementation.InsertMotorcycle(MotorcycleMapper.JsonToDocumentMapper(motorcycle));
                
                NotificationDocument notificationDocument = new NotificationDocument
                {
                    Message = "Moto cadastrada com sucesso!",
                    LicensePlate = motorcycle.LicensePlate
                };
                string json = JsonSerializer.Serialize(notificationDocument);

                GenericPublisher.PublishMessage(json, "motorcycle");

                if(motorcycle.Year == 2024)
                {
                    notificationDocument.Message = "Atenção! Moto do ano de 2024 cadastrada.";
                    json = JsonSerializer.Serialize(notificationDocument);

                    GenericPublisher.PublishMessage(json, "motorcycle");    
                }

                return StatusCode(201);
            }
            catch (Exception)
            {
                return BadRequest(new { mensagem = "Dados inválidos" });
            }
        }

        [HttpGet]
        public IActionResult GetMotorcycles([FromQuery] string licensePlate)
        {
            try
            {
                MotorcycleDocument motorcycleDoc = _motorcycleImplementation.GetByLicensePlate(licensePlate);
                Motorcycle motorcycle = MotorcycleMapper.DocumentToJsonMapper(motorcycleDoc);

                return Ok(motorcycle);
            }
            catch (Exception)
            {
                return BadRequest(new { mensagem = "Dados inválidos" });
            }
        }

        [HttpPut("{id}/placa")]
        public IActionResult UpdateLicensePlate(string id, [FromBody] UpdatePlateRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id) || request == null || string.IsNullOrWhiteSpace(request.LicensePlate))
                    throw new Exception();

                MotorcycleDocument motrocycle = _motorcycleImplementation.GetById(id);

                if (motrocycle == null)
                    throw new Exception();

                _motorcycleImplementation.UpdateLicensePlate(id, request.LicensePlate);

                return Ok(new { mensagem = "Placa modificada com sucesso" });
            }
            catch (Exception)
            {
                return BadRequest(new { mensagem = "Dados inválidos" });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetMotorcycleById(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                    return BadRequest(new { mensagem = "Request mal formatada" });

                MotorcycleDocument motorcycleDoc = _motorcycleImplementation.GetById(id);

                if (motorcycleDoc == null)
                    return NotFound(new { mensagem = "Moto não encontrada" });

                Motorcycle motorcycle = MotorcycleMapper.DocumentToJsonMapper(motorcycleDoc);
                return Ok(motorcycle);
            }
            catch
            {
                return BadRequest(new { mensagem = "Request mal formatada" });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMotorcycle(string id)
        {
            try
            {
                MotorcycleDocument motorcycle = _motorcycleImplementation.GetById(id);
                if (motorcycle == null)
                    throw new Exception();

                _motorcycleImplementation.DeleteById(id);

                return Ok(new { mensagem = "Moto removida com sucesso" });
            }
            catch
            {
                return BadRequest(new { mensagem = "Dados inválidos" });
            }
        }
    }
}
