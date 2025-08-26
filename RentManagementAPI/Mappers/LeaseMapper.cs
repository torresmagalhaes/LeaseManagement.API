using LeaseManagement.Domain.Entities;
using LeaseManagement.Domain.Entities.MongoDB;

namespace LeaseManagementAPI.Mappers
{
    public class LeaseMapper
    {
        public static LeaseDocument JsonToDocumentMapper(Lease lease)
        {
            LeaseDocument leaseDocument = new LeaseDocument 
            {
                Identifier = lease.Identifier,
                DeliveryManId = lease.DeliveryManId,
                MotorcycleId = lease.MotorcycleId,
                StartDate = lease.StartDate,
                EndDate = lease.EndDate,
                ExpectedEndDate = lease.ExpectedEndDate,
                Plan = lease.Plan
            };

            return leaseDocument;
        }

        public static Lease DocumentToJsonMapper(LeaseDocument leaseDocument)
        {
            return new Lease
            {
                DeliveryManId = leaseDocument.DeliveryManId,
                MotorcycleId = leaseDocument.MotorcycleId,
                StartDate = leaseDocument.StartDate,
                EndDate = leaseDocument.EndDate,
                ExpectedEndDate = leaseDocument.ExpectedEndDate,
                Plan = leaseDocument.Plan
            };
        }
    }
}
