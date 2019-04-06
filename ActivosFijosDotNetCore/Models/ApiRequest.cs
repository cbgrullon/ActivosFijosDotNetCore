using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivosFijosDotNetCore.Models
{
    public class Detalle
    {

        [JsonProperty("numero_cuenta")]
        public string NumeroCuenta { get; set; }

        [JsonProperty("tipo_transaccion")]
        public string TipoTransaccion { get; set; }

        [JsonProperty("monto")]
        public int Monto { get; set; }
    }

    public class ApiRequest
    {

        [JsonProperty("auxiliar")]
        public string Auxiliar { get; set; }

        [JsonProperty("moneda")]
        public string Moneda { get; set; }

        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("detalle")]
        public List<Detalle> Detalle { get; set; }
    }
}
