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

        public void UpdateLicensePlate(string id, string newPlate)
        {
            var update = Builders<MotorcycleDocument>.Update.Set(x => x.LicensePlate, newPlate);
            _motorcycleCollection.UpdateOne(x => x.Identifier == id, update);
        }

        public MotorcycleDocument? GetByLicensePlate(string licensePlate)
        {
            return _motorcycleCollection.Find(x => x.LicensePlate == licensePlate).FirstOrDefault();
        }

        public MotorcycleDocument? GetById(string id)
        {
            return _motorcycleCollection.Find(x => x.Identifier == id).FirstOrDefault();
        }

        public void DeleteById(string id)
        {
            _motorcycleCollection.DeleteOne(x => x.Identifier == id);
        }
    }
}