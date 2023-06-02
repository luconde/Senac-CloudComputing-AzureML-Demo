namespace Senac_ConsultaPrecoProjetado.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class CGlobalParameters
    {
    }
    public class CInput1
    {
        public string id { get; set; }
        public double preco { get; set; }
        public int quartos { get; set; }
        public int banheiros { get; set; }
        public int areautil { get; set; }
        public int areatotal { get; set; }
        public int andares { get; set; }
        public int anos { get; set; }
        public int renovada { get; set; }
    }

    public class CInputs
    {
        public List<CInput1> input1 { get; set; }

        public CInputs()
        {
            input1 = new List<CInput1>();
        }
    }

    public class CAzureMLRequest
    {
        public CInputs Inputs { get; set; }
        public CGlobalParameters GlobalParameters { get; set; }

        public CAzureMLRequest()
        {
            Inputs = new CInputs();
            GlobalParameters = new CGlobalParameters();
        }
    }


}
