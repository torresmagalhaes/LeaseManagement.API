﻿using System;
using System.Text.Json.Serialization;

namespace LeaseManagement.Domain.Entities
{
    public class Lease
    {
        [JsonPropertyName("entregador_id")]
        public string DeliveryManId { get; set; }

        [JsonPropertyName("moto_id")]
        public string MotorcycleId { get; set; }

        [JsonPropertyName("data_inicio")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("data_termino")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("data_previsao_termino")]
        public DateTime ExpectedEndDate { get; set; }

        [JsonPropertyName("plano")]
        public int Plan { get; set; }
    }
}
