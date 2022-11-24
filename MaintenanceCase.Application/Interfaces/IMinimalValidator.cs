using FluentValidation.Results;

namespace Unidas.MS.Maintenance.Case.Application.Interfaces
{
    public interface IMinimalValidator
    {
        ValidationResult Validate<T>(T model);
    }
}
