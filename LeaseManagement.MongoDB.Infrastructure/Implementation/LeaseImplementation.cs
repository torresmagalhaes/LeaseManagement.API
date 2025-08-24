using LeaseManagement.Domain.Entities.MongoDB;
using LeaseManagement.Infrastructure.MongoDB.Data;
using MongoDB.Driver;

namespace LeaseManagement.Infrastructure.MongoDB.Implementation
{
    public class LeaseImplementation
    {
        private readonly IMongoCollection<LeaseDocument> _leaseCollection;

        public LeaseImplementation(MongoDBService mongoDBService)
        {
            _leaseCollection = mongoDBService.Database.GetCollection<LeaseDocument>("LeaseDocument");
        }

        public void InsertLease(LeaseDocument leaseDocument)
        {
            _leaseCollection.InsertOne(leaseDocument);
        }
    }
}   