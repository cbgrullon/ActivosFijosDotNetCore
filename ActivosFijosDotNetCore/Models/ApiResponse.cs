using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivosFijosDotNetCore.Models
{
    public class ApiResponse
    {
        [JsonProperty("resultado")]
        public string Resultado { get; set; }

        [JsonProperty("mensaje")]
        public string Mensaje { get; set; }
    }
}
