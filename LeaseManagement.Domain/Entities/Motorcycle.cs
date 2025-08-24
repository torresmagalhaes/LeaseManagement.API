﻿using System.Text.Json.Serialization;

namespace LeaseManagement.Domain.Entities
{
    public class Motorcycle
    {
        [JsonPropertyName("identificador")]
        public string Identifier { get; set; }

        [JsonPropertyName("ano")]
        public int Year { get; set; }

        [JsonPropertyName("modelo")]
        public string Model { get; set; }

        [JsonPropertyName("placa")]
        public string LicensePlate { get; set; }
    }
}
