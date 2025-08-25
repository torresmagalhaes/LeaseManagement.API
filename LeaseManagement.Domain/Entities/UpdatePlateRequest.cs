using System.Text.Json.Serialization;

namespace LeaseManagement.Domain.Entities
{
    public class UpdatePlateRequest
    {
        [JsonPropertyName("placa")]
        public string LicensePlate { get; set; }
    }
}