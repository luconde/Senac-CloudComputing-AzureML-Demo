using Newtonsoft.Json;

namespace Senac_ConsultaPrecoProjetado.Models
{
    public class CAzureMLResponse
    {
        public CResults Results { get; set; }
    }

    public class CResults
    {
        public List<CWebServiceOutput0> WebServiceOutput0 { get; set; }
    }

    public class CWebServiceOutput0
    {
        public double preco { get; set; }
        public double quartos { get; set; }
        public double banheiros { get; set; }
        public double areautil { get; set; }
        public double anos { get; set; }

        [JsonProperty("Scored Labels")]
        public double ScoredLabels { get; set; }
    }
}
