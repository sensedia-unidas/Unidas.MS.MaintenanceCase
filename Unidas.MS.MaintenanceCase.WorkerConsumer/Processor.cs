using MaintenanceCase.Domain.Enums;
using Newtonsoft.Json;
using Unidas.MS.Maintenance.Case.Application.ViewModels;
using Unidas.MS.Maintenance.Case.Application.ViewModels.Requests;
using Unidas.MS.Maintenance.Case.Application.ViewModels.Responses;

namespace Unidas.MS.Maintenance.Case.WorkerConsumer
{
    public class Processor : Worker
    {

        protected readonly ILogger<Worker> _logger;
        protected readonly AppSettings _appSettings;          

        public Processor(ILogger<Worker> logger, IServiceScopeFactory factory) : base(logger, factory)
        {
            _logger = logger;
            _appSettings = new AppSettings();
            _logger.LogInformation("Maintenance Case Service - Processing integration messages");      
            
        }

        protected override async Task<bool> ProcessMessage(ItemMaintenanceCaseRequestViewModel request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("New " + request.Action + " request from " + request.Origin + " to " + request.Destiny);

            if(request.Origin == Systems.SalesForce && request.Destiny == Systems.JiraService && request.Action == Actions.Create)
            {              
                                
               bool response = await _maintenanceCaseServiceJira.Integrate(request);

               if (!response)
               {
                   _logger.LogInformation("Falha no processamento do item: {0}", request);
                   return false;
               }

               _logger.LogInformation("Processing completed successfully", request);
               return true;

            }


            if (request.Origin == Systems.AppOnRoad && request.Destiny == Systems.SalesForce)
            {
                TokenResponse tokenResponse = await _tokenSalesforceService.GetToken();
                //TokenResponse tokenResponse = new TokenResponse();
                //tokenResponse.token = "00D8J0000008fo5!ARIAQCaFsdGidt6V2gpYZ46PeRh9BtF_Iwo61imNCA4bxNPfUxHcTdsC98gpoMAZsen_sXkranoOB_iUVfwAr2Oqo2Yi8gtG";

                if(tokenResponse != null)
                {
                    _logger.LogInformation("Token SalesForce: " + tokenResponse.token);


                    bool response = await _maintenanceCaseServiceSalesforce.Integrate(request, tokenResponse.token);

                    if (!response)
                    {
                        _logger.LogInformation("Item processing failure: {0}", JsonConvert.SerializeObject(request));
                        return false;
                    }

                    _logger.LogInformation("Processing completed successfully", request);

                    return true;
                } else
                {
                    return false;
                }
               

            }

            // await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
            return false;            

        }
    }
}