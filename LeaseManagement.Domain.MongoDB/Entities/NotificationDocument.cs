using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseManagement.Domain.MongoDB.Entities
{
    public class NotificationDocument
    {
        [BsonElement("Mensagem"), BsonRepresentation(BsonType.String)]
        public string Message { get; set; }
        [BsonElement("Placa"), BsonRepresentation(BsonType.String)]
        public string LicensePlate { get; set; }
    }
}
