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

        public LeaseDocument? GetById(string id)
        {
            return _leaseCollection.Find(x => x.Identifier == id).FirstOrDefault();
        }

        public LeaseDocument? GetByMotorcycleId(string motorcycleId)
        {
            return _leaseCollection.Find(x => x.MotorcycleId == motorcycleId).FirstOrDefault();
        }

        public void UpdateEndDate(string id, DateTime devolutionDay)
        {
            var update = Builders<LeaseDocument>.Update.Set(x => x.EndDate, devolutionDay);
            _leaseCollection.UpdateOne(x => x.Identifier == id, update);
        }
    }
}   