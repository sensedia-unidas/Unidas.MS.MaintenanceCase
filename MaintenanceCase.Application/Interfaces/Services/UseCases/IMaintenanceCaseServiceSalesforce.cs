
using Unidas.MS.Maintenance.Case.Application.ViewModels.Requests;

namespace Unidas.MS.Maintenance.Case.Application.Interfaces.Services.UseCases
{
    public interface IMaintenanceCaseServiceSalesforce
    {
        Task<bool> Integrate(ItemMaintenanceCaseRequestViewModel maintenanceCaseRequestViewModel, string token);
    }
}
