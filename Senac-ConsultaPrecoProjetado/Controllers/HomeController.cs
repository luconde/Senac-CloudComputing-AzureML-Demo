using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Senac_ConsultaPrecoProjetado.Models;
using System.Diagnostics;

namespace Senac_ConsultaPrecoProjetado.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration pobjConfiguration;
        private readonly IWebHostEnvironment pobjWebHostingConfiguration;

        public HomeController(ILogger<HomeController> logger, IConfiguration objConfiguration, IWebHostEnvironment env )
        {
            this._logger = logger;
            this.pobjWebHostingConfiguration = env;
            this.pobjConfiguration = objConfiguration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetAsync([Bind("Quartos", "Banheiros", "AreaUtil", "AreaTotal", "Anos")] CFormImovel objImovelForm)
        {
            string strResponse;
            string strMessage;
            bool blnError;
            double dblForecast = 0;


            // Inicia o objeto Http para tratar as requisições
            var handler = new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =
                    (httpRequestMessage, cert, cetChain, policyErros) => { return true; }
            };


            using (var client = new HttpClient(handler))
            {

                // Cria os dados de input do imovel
                CInput1 input1 = new CInput1();
                input1.id = string.Empty;
                input1.preco = 0;
                input1.quartos = Convert.ToInt32(objImovelForm.Quartos);
                input1.areautil = Convert.ToInt32(objImovelForm.AreaUtil);
                input1.areatotal = Convert.ToInt32(objImovelForm.AreaTotal);
                input1.anos = Convert.ToInt32(objImovelForm.Anos);
                input1.andares = 0;
                input1.renovada = 0;

                CAzureMLRequest objAzure = new CAzureMLRequest();
                objAzure.Inputs.input1.Add(input1);

                // transforma em modelo JSON
                var requestBody = JsonConvert.SerializeObject(objAzure);

                // Guardado para referencia de debut

                //var requestBody = @"{
                //    ""Inputs"": {
                //        ""input1"" : [
                //            {
                //                ""id"": ""7129300520"",
                //                ""preco"": 221900.0,
                //                ""quartos"": 3,
                //                ""banheiros"": 1,
                //                ""areautil"": 110,
                //                ""areatotal"": 525,
                //                ""andares"": 1,
                //                ""anos"": 68,
                //                ""renovada"": 0
                //            } 
                //        ]
                //    },
                //    ""GlobalParameters"": {}
                //}";


                // Conecta no Azure ML para enviar a requisição
                string apiKey = pobjConfiguration.GetValue<string>("AzureMachineLearningConnectionString:ApiKey");

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
                client.BaseAddress = new Uri(pobjConfiguration.GetValue<string>("AzureMachineLearningConnectionString:Url"));

                var content = new StringContent(requestBody);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await client.PostAsync("", content);

                // Em caso de sucesso, converte o JSON de saida em classes C#
                if(response.IsSuccessStatusCode)
                {
                    strResponse = await response.Content.ReadAsStringAsync();
                    CAzureMLResponse objResult = JsonConvert.DeserializeObject<CAzureMLResponse>(strResponse);

                    blnError = false;
                    strMessage = string.Empty;
                    dblForecast = objResult.Results.WebServiceOutput0[0].ScoredLabels;
                }
                else
                {
                    strResponse = await response.Content.ReadAsStringAsync();
                    blnError = true;
                    strMessage = strResponse;
                }


            }
            ViewBag.Error = blnError;
            ViewBag.Message = strMessage;
            ViewBag.Forecast = dblForecast;

            return View();

        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}