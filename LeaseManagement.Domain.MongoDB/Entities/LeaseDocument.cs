using LeaseManagement.Domain.MongoDB.Entities.BaseDocument;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace LeaseManagement.Domain.Entities.MongoDB
{
    public class LeaseDocument : BaseDocument
    {
        [BsonElement("identificação"), BsonRepresentation(BsonType.String)]
        public string Identifier { get; set; }

        [BsonElement("entregador_id"), BsonRepresentation(BsonType.String)]
        public string DeliveryManId { get; set; }

        [BsonElement("moto_id"), BsonRepresentation(BsonType.String)]
        public string MotorcycleId { get; set; }

        [BsonElement("data_inicio"), BsonRepresentation(BsonType.DateTime)]
        public DateTime StartDate { get; set; }

        [BsonElement("data_termino"), BsonRepresentation(BsonType.DateTime)]
        public DateTime EndDate { get; set; }

        [BsonElement("data_previsao_termino"), BsonRepresentation(BsonType.DateTime)]
        public DateTime ExpectedEndDate { get; set; }

        [BsonElement("plano"), BsonRepresentation(BsonType.Int32)]
        public int Plan { get; set; }
    }
}
