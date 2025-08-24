using Microsoft.AspNetCore.Mvc;
using LeaseManagement.Domain.Entities;
using LeaseManagementAPI.Validations;
using Microsoft.AspNetCore.Components.Forms;
using LeaseManagement.Infrastructure.MongoDB.Data;
using MongoDB.Driver;
using LeaseManagement.Domain.Entities.MongoDB;
using LeaseManagementAPI.Mappers;
using LeaseManagement.Infrastructure.MongoDB.Implementation;

namespace RentManagementAPI.Controllers
{
    [ApiController]
    [Route("entregadores")]
    public class DeliveryManController : ControllerBase
    {
        private readonly DeliveryManImplementation _deliveryManImplementation;
        public DeliveryManController(DeliveryManImplementation deliveryManImplementation)
        {
            _deliveryManImplementation = deliveryManImplementation;
        }

        [HttpPost]
        public IActionResult RegisterDeliveryMan([FromBody] DeliveryMan deliveryMan)
        {
            try
            {
                APIValidator.ValidateDeliveryManRegister(deliveryMan);
                DeliveryManDocument deliveryManDocument = DeliveryManMapper.JsonToDocumentMapper(deliveryMan);
                _deliveryManImplementation.InsertDeliveryMan(deliveryManDocument);

                return StatusCode(201);
            }
            catch (Exception)
            {
                return BadRequest(new { mensagem = "Dados inválidos" }); 
            }
        }

        [HttpPost("/{id}/cnh")]
        public IActionResult UploadCNHPhoto(string id, [FromBody] CNHPhotoRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id) || request == null || string.IsNullOrWhiteSpace(request.imagem_cnh))
                    throw new Exception();

                var deliveryMan = _deliveryManImplementation.GetByIdentifier(id);
                if (deliveryMan == null)
                    throw new Exception();

                // Atualiza a imagem da CNH

                return StatusCode(201);
            }
            catch (Exception)
            {
                return BadRequest(new { mensagem = "Dados inválidos" });
            }
        }
    }

    public class CNHPhotoRequest
    {
        public string imagem_cnh { get; set; }
    }
}
