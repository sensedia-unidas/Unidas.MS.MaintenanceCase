
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Unidas.MS.Maintenance.Case.Application.Interfaces.Services.Case;
using Unidas.MS.Maintenance.Case.Application.ViewModels;
using Unidas.MS.Maintenance.Case.Application.ViewModels.Requests;

namespace Unidas.MS.Maintenance.Case.Application.Services.UseCases
{
    public class MaintenanceCaseServiceJira : IMaintenanceCaseServiceJira
    {
        private readonly ILogger<MaintenanceCaseServiceJira> _logger;
        private readonly IConfiguration _configuration;
        private readonly AppSettings appSettings;
       
        public MaintenanceCaseServiceJira(ILogger<MaintenanceCaseServiceJira> logger, IConfiguration configuration) 
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<bool> Integrate(ItemMaintenanceCaseRequestViewModel maintenanceCaseRequestViewModel)
        {
            var jiraClientEndPoint = new JIRAPortalCliente.ServicoPortalClientePortTypeClient.EndpointConfiguration();

            var jiraClient = new JIRAPortalCliente.ServicoPortalClientePortTypeClient(jiraClientEndPoint, appSettings.JiraIntegrationUrl);

            _logger.LogInformation("Processing Jira Integration - Case");

            return true;

            /*

            try
            {
                var jiraRequest = await jiraClient.CriaDadosAtendimentoAsync(
                    maintenanceCaseViewModel.Plate,
                    maintenanceCaseViewModel.Driver.Cpf,
                    maintenanceCaseViewModel.Odometer,
                    string.Empty,                    
                    string.Empty,
                    maintenanceCaseViewModel.CarWorkshopScheduleDateTime,
                    maintenanceCaseViewModel.SupplierCnpj,
                    string.Empty, // State
                    string.Empty, // City
                    string.Empty, // ThirdParties
                    string.Empty)
                    .ConfigureAwait(false);
            }

            catch (Exception ex)
            {

            }
            */
        }


    }
}
