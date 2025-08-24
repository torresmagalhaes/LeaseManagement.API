using LeaseManagement.Domain.Entities;
using LeaseManagement.Domain.Entities.MongoDB;

namespace LeaseManagementAPI.Mappers
{
    public class DeliveryManMapper
    {
        public static DeliveryManDocument JsonToDocumentMapper(DeliveryMan deliveryMan)
        {
            DeliveryManDocument deliveryManDocument = new DeliveryManDocument
            {
                Id = Guid.NewGuid().ToString(),
                Identifier = deliveryMan.Identifier,
                Name = deliveryMan.Name,
                TaxNumber = deliveryMan.TaxNumber,
                BirthDate = deliveryMan.Birthday,
                CNHType = deliveryMan.CNHType
            };

            return deliveryManDocument;
        }
    }
}
