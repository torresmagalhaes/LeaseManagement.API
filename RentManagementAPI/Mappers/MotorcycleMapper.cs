using LeaseManagement.Domain.Entities;
using LeaseManagement.Domain.Entities.MongoDB;

namespace LeaseManagementAPI.Mappers
{
    public class MotorcycleMapper
    {
        public static MotorcycleDocument JsonToDocumentMapper(Motorcycle motorcycle)
        {
            MotorcycleDocument motorcycleDocument = new MotorcycleDocument
            {
                Id = Guid.NewGuid().ToString(),
                Model = motorcycle.Model,
                Year = motorcycle.Year,
                LicensePlate = motorcycle.LicensePlate,
            };

            return motorcycleDocument;
        }
    }
}
