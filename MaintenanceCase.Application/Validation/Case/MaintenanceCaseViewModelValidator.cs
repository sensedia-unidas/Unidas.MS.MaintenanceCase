using FluentValidation;
using Unidas.MS.Maintenance.Case.Application.ViewModels.Requests;

namespace Maintenance.Case.Application.Validation.Case
{
    public class MaintenanceCaseViewModelValidator : AbstractValidator<ItemMaintenanceCaseRequestViewModel>
    {
        public MaintenanceCaseViewModelValidator()
        {
            RuleFor(x => x.Origin).NotNull();
            RuleFor(x => x.Destiny).NotNull();
        }

    }
}
