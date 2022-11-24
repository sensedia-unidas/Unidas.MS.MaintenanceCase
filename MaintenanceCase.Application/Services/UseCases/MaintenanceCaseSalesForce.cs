

using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Unidas.MS.Maintenance.Case.Application.Interfaces.Services.UseCases;
using Unidas.MS.Maintenance.Case.Application.ViewModels;
using Unidas.MS.Maintenance.Case.Application.ViewModels.MaintenanceCase;
using Unidas.MS.Maintenance.Case.Application.ViewModels.Requests;
using System.Net.Http.Headers;
using System.Text;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Unidas.MS.Maintenance.Case.Application.Services.UseCases
{
    public class MaintenanceCaseSalesForce : IMaintenanceCaseServiceSalesforce
    {
        private readonly AppSettings _appSettings;
        private readonly HttpClient _httpClient;
        protected readonly ILogger<MaintenanceCaseSalesForce> _logger;

        public MaintenanceCaseSalesForce(ILogger<MaintenanceCaseSalesForce> logger, AppSettings appSettings, HttpClient httpClient)
        {
            _appSettings = appSettings;
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<bool> Integrate(ItemMaintenanceCaseRequestViewModel maintenanceCaseRequest, string token)
        {
            _logger.LogInformation("Sending maintenance case request to SalesForce");
           
            CaseSalesforce caseRequest = JsonConvert.DeserializeObject<CaseSalesforce>(maintenanceCaseRequest.Payload.ToString());
            // _logger.LogInformation("CarWorkshopScheduleDateTime: {0}", caseRequest.CarWorkshopScheduleDateTime);

            try
            {
                 _httpClient.BaseAddress = new Uri(_appSettings.SalesForce.UrlCaseAppOnRoad);
                 _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                 string idSalesForce = string.Empty;

                 // Preparing request
                 StringContent content = FormatRequest(caseRequest);
                    
                 // Sending
                 var response = await _httpClient.PostAsync(_appSettings.SalesForce.UrlCaseAppOnRoad, content);

                 _logger.LogInformation(JsonConvert.SerializeObject(response));
                _logger.LogInformation(response.StatusCode.ToString());
                              
                var resultObject = JToken.Parse(await response.Content.ReadAsStringAsync());
                idSalesForce = resultObject.Root.Root["message"].ToString();
                _logger.LogInformation("Case created in Salesforce with id: {0}", idSalesForce);
                return true;
                
                    
            }catch (Exception ex)
            {

                _logger.LogError(ex.Message);

                return false;
            }

           

           
        }


        private StringContent FormatRequest(CaseSalesforce caseRequest)
        {
            if (!caseRequest.Driver.Cpf.Contains("-"))
            {
                caseRequest.Driver.Cpf = caseRequest.Driver.Cpf.Insert(9, "-");
            }

            List<dynamic> CategoryList = new List<dynamic>();
            var maintenanceType = "";
            var driver = caseRequest.Driver;

            foreach (var category in caseRequest.Categories)
            {
                if (maintenanceType == "" && category.TypeOfService >= 0)
                    maintenanceType = category.IsCarService ? "Preventiva" : "Corretiva";

                CategoryList.Add(new
                {
                    IdExterno__c = category.Recid,
                    Prioridade__c = category.Priority
                });
            }

            var jsonRequest = JsonConvert.SerializeObject(new
            {
                caseInput = new
                {
                    subcategoria = maintenanceType,
                    dataAgendamento = caseRequest.CarWorkshopScheduleDateTime,
                    categorias = CategoryList,
                    condutor = caseRequest.Driver.Cpf.Replace("-", ""),
                    kmAtual = caseRequest.Odometer,
                    ativo = new { Placa__c = caseRequest.Plate },
                    descricao = caseRequest.Comment,
                    origem = "APP",
                    fornecedor = "FC" + caseRequest.SupplierCnpj,
                    contato = driver != null ? new
                    {
                        CPF__c = driver.Cpf.Replace("-", ""),
                        MobilePhone = driver.CellPhone,
                        email = driver.Email,
                        FirstName = driver.FullName.Split(" ").FirstOrDefault(),
                        LastName = driver.FullName.Split(" ").LastOrDefault(),
                    } : null
                }
            });

            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            _logger.LogInformation(JsonConvert.SerializeObject(jsonRequest));

            return content;
        }
    }
}
