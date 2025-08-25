using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LeaseManagement.Domain.Entities
{
    public class CNHPhotoRequest
    {
        [JsonPropertyName("imagem_cnh")]
        public string CNHPhoto { get; set; }
    }
}
