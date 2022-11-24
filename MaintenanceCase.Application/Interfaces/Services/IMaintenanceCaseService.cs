using FluentValidation.Results;
using ValidationResult = FluentValidation.Results.ValidationResult;
using Unidas.MS.Maintenance.Case.Application.ViewModels.Requests;

namespace Unidas.MS.Maintenance.Application.Interfaces.Services
{
    public interface IMaintenanceCaseService
    {
        Task<ValidationResult> Integrate(ItemMaintenanceCaseRequestViewModel request);
    }
}
