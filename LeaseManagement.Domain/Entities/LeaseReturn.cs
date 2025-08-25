using System;
using System.Text.Json.Serialization;

namespace LeaseManagement.Domain.Entities
{
    public class LeaseReturn
    {
        [JsonPropertyName("data_devolucao")]
        public DateTime DevolutionDay{ get; set; }
    }
}
