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
        public CNHCategory CNHType { get; set; }

        [JsonPropertyName("imagem_cnh")]
        public Base64StringAttribute CNHPhoto { get; set; }
    }

    public enum CNHCategory
    {
        A,
        B,
        C,
        D,
        E
    }
}
