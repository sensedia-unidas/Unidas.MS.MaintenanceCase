

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Unidas.MS.Maintenance.Case.Application.Interfaces.Services.UseCases;
using Unidas.MS.Maintenance.Case.Application.ViewModels;
using Unidas.MS.Maintenance.Case.Application.ViewModels.Requests;
using Unidas.MS.Maintenance.Case.Application.ViewModels.Responses;

namespace Unidas.MS.Maintenance.Case.Application.Services.UseCases
{
    public class TokenSalesforceService : ITokenSalesforceService
    {

        private readonly AppSettings _appSettings;
        private readonly HttpClient _httpClient;
        protected readonly ILogger<TokenSalesforceService> _logger;
       

        public TokenSalesforceService(HttpClient httpClient, AppSettings appSettings, ILogger<TokenSalesforceService> logger)
        {
            _appSettings = appSettings;
            _logger = logger;   
            _httpClient = httpClient;
        }

        public async Task<TokenResponse?> GetToken()
        {
            
            try
            {
                _logger.LogInformation("Requesting token for SalesForce");

                TokenRequest request = new TokenRequest();
                request.clientId = _appSettings.SalesForce.clientId;
                request.userName = _appSettings.SalesForce.userName;
                request.clientSecret = _appSettings.SalesForce.clientSecret;
                request.password = _appSettings.SalesForce.password;

                StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(request).ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(_appSettings.SalesForce.MSTokenUrl, jsonContent).ConfigureAwait(false);

                var jsonResponse = await response.Content.ReadAsStringAsync();

                TokenResponse tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(jsonResponse);


                return tokenResponse;
            } catch (Exception ex)
            {
                _logger.LogError("Error on get token from MS SalesForce Authentication",ex.Message);
                return null;
            }
            
            
            

        }

           

           
    }
}
