using Unidas.MS.Maintenance.Case.Application.ViewModels.Requests;

namespace Unidas.MS.Maintenance.Case.Application.Interfaces.Services.Case
{
    public interface IMaintenanceCaseServiceJira
    {
        Task<bool> Integrate(ItemMaintenanceCaseRequestViewModel maintenanceCaseRequestViewModel);

    }
}
