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
                Identifier = motorcycle.Identifier,
                Model = motorcycle.Model,
                Year = motorcycle.Year,
                LicensePlate = motorcycle.LicensePlate,
            };

            return motorcycleDocument;
        }

        public static Motorcycle DocumentToJsonMapper(MotorcycleDocument motorcycleDocument)
        {
            Motorcycle motorcycle = new Motorcycle
            {
                Identifier = Guid.NewGuid().ToString(),
                Model = motorcycleDocument.Model,
                Year = motorcycleDocument.Year,
                LicensePlate = motorcycleDocument.LicensePlate,
            };

            return motorcycle;
        }
    }
}
