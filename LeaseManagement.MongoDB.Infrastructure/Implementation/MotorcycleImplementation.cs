using LeaseManagement.Domain.Entities.MongoDB;
using LeaseManagement.Infrastructure.MongoDB.Data;
using MongoDB.Driver;

namespace LeaseManagement.Infrastructure.MongoDB.Implementation
{
    public class MotorcycleImplementation
    {
        private readonly IMongoCollection<MotorcycleDocument> _motorcycleCollection;

        public MotorcycleImplementation(MongoDBService mongoDBService)
        {
            _motorcycleCollection = mongoDBService.Database.GetCollection<MotorcycleDocument>("MotorcycleDocument");
        }

        public void InsertMotorcycle(MotorcycleDocument motorcycleDocument)
        {
            _motorcycleCollection.InsertOne(motorcycleDocument);
        }
    }
}