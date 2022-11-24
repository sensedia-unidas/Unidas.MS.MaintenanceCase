
using Unidas.MS.Maintenance.Case.Application.ViewModels.Responses;

namespace Unidas.MS.Maintenance.Case.Application.Interfaces.Services.UseCases
{
    public interface ITokenSalesforceService
    {
        Task<TokenResponse> GetToken();
    }
}
