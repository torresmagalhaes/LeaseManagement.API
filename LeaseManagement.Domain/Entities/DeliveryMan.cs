using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LeaseManagement.Domain.Entities
{
    public class DeliveryMan
    {
        [JsonPropertyName("identificador")]
        public string Identifier { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [JsonPropertyName("cnpj")]
        public string TaxNumber { get; set; }

        [JsonPropertyName("data_nascimento")]
        public DateTime Birthday { get; set; }

        [JsonPropertyName("tipo_cnh")]
        public string CNHType { get; set; }

        [JsonPropertyName("imagem_cnh")]
        public string CNHPhoto { get; set; }
    }
}
