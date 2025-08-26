using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace LeaseManagement.Domain.Entities.MongoDB
{
    public class MotorcycleDocument
    {
        [BsonElement("identificador"), BsonRepresentation(BsonType.String)]
        public string Identifier { get; set; }

        [BsonElement("ano"), BsonRepresentation(BsonType.Int32)]
        public int Year { get; set; }

        [BsonElement("modelo"), BsonRepresentation(BsonType.String)]
        public string Model { get; set; }

        [BsonElement("placa"), BsonRepresentation(BsonType.String)]
        public string LicensePlate { get; set; }
    }
}
