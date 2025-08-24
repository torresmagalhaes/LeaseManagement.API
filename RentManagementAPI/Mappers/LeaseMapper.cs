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
                Id = Guid.NewGuid().ToString(),
                DeliveryManId = lease.DeliveryManId,
                MotorcycleId = lease.MotorcycleId,
                StartDate = lease.StartDate,
                EndDate = lease.EndDate,
                ExpectedEndDate = lease.ExpectedEndDate,
                Plan = lease.Plan
            };

            return leaseDocument;
        }
    }
}
