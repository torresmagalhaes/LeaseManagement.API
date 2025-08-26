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

                if (deliveryMan.CNHPhoto != null)
                {
                    var base64 = deliveryMan.CNHPhoto.ToString();
                    if (!string.IsNullOrWhiteSpace(base64))
                    {
                        var bytes = Convert.FromBase64String(base64);

                        bool isJpg = bytes.Length > 3 &&
                                     bytes[0] == 0xFF && bytes[1] == 0xD8 && bytes[2] == 0xFF;
                        bool isBmp = bytes.Length > 2 &&
                                     bytes[0] == 0x42 && bytes[1] == 0x4D;

                        if (!isJpg && !isBmp)
                            throw new Exception();

                        var extension = isJpg ? "jpg" : "bmp";
                        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "cnh_photos");

                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        var filePath = Path.Combine(folderPath, $"{deliveryMan.Identifier}_cnh.{extension}");
                        System.IO.File.WriteAllBytes(filePath, bytes);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }

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
        public IActionResult UpdateCNHPhoto(string id, [FromBody] CNHPhotoRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id) || request == null || string.IsNullOrWhiteSpace(request.CNHPhoto))
                    throw new Exception();

                var deliveryMan = _deliveryManImplementation.GetByIdentifier(id);
                if (deliveryMan == null)
                    throw new Exception();

                var bytes = Convert.FromBase64String(request.CNHPhoto);

                bool isPng = bytes.Length > 8 &&
                             bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47 &&
                             bytes[4] == 0x0D && bytes[5] == 0x0A && bytes[6] == 0x1A && bytes[7] == 0x0A;

                bool isBmp = bytes.Length > 2 &&
                             bytes[0] == 0x42 && bytes[1] == 0x4D;

                if (!isPng && !isBmp)
                    throw new Exception();

                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "cnh_photos");
                Directory.CreateDirectory(folderPath);

                var extension = isPng ? "png" : "bmp";
                var filePath = Path.Combine(folderPath, $"{id}_cnh.{extension}");

                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                System.IO.File.WriteAllBytes(filePath, bytes);

                return StatusCode(201);
            }
            catch (Exception)
            {
                return BadRequest(new { mensagem = "Dados inválidos" });
            }
        }
    }
}
