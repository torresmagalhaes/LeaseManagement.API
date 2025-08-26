using LeaseManagement.Domain.MongoDB.Entities.BaseDocument;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LeaseManagement.Domain.Entities.MongoDB
{
    public class DeliveryManDocument : BaseDocument
    {
        [BsonElement("identificador"), BsonRepresentation(BsonType.String)]
        public string Identifier { get; set; }
        [BsonElement("nome"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }
        [BsonElement("cnpj"), BsonRepresentation(BsonType.String)]
        public string TaxNumber { get; set; }
        [BsonElement("data_nascimento"), BsonRepresentation(BsonType.DateTime)]
        public DateTime BirthDate { get; set; }
        [BsonElement("tipo_cnh"), BsonRepresentation(BsonType.String)]
        public string CNHType { get; set; }
    }
}

