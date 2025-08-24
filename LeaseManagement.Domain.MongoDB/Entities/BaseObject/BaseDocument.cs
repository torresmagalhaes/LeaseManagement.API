using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LeaseManagement.Domain.Entities.MongoDB.BaseObject
{
    public class BaseDocument
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
    }
}
