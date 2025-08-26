using LeaseManagement.Domain.Entities.MongoDB;
using LeaseManagement.Infrastructure.MongoDB.Data;
using MongoDB.Driver;

namespace LeaseManagement.Infrastructure.MongoDB.Implementation
{
    public class DeliveryManImplementation
    {
        private readonly IMongoCollection<DeliveryManDocument> _deliveryManCollection;

        public DeliveryManImplementation(MongoDBService mongoDBService)
        {
            _deliveryManCollection = mongoDBService.Database.GetCollection<DeliveryManDocument>("DeliveryManDocument");
        }

        public void InsertDeliveryMan(DeliveryManDocument deliveryManDocument)
        {
            _deliveryManCollection.InsertOne(deliveryManDocument);
        }

        public DeliveryManDocument? GetByIdentifier(string identifier)
        {
            return _deliveryManCollection.Find(x => x.Identifier == identifier).FirstOrDefault();
        }

        public void UpdateEndDate(string id, DateTime devolutionDay)
        {
            var update = Builders<LeaseDocument>.Update.Set(x => x.EndDate, devolutionDay);
            _motorcycleCollection.UpdateOne(x => x.Identifier == id, update);
        }
    }
}
