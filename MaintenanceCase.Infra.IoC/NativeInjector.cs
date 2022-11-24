
using Microsoft.Extensions.DependencyInjection;
using MaintenanceCase.Application.Validation;
using Unidas.MS.Maintenance.Case.Application.Services.UseCases;
using Unidas.MS.Maintenance.Case.Application.Interfaces.Services.UseCases;
using Unidas.MS.Maintenance.Case.Application.Interfaces.Services.Case;
using Unidas.MS.Maintenance.Case.Application.Interfaces;

namespace Unidas.MS.Maintenance.Case.Infra.IoC
{
    public class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //REPOSITORY


            //SERVICE
            services.AddScoped<IMaintenanceCaseServiceJira, MaintenanceCaseServiceJira>();            
            services.AddScoped<ITokenSalesforceService, TokenSalesforceService>();
            services.AddScoped<IMaintenanceCaseServiceSalesforce, MaintenanceCaseSalesForce>();

            //VALIDATOR
            IServiceCollection serviceCollection = services.AddScoped<IMinimalValidator, MinimalValidator>();

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                //cfg.AddProfile(new DomainToViewModelMappingProfile());
                //cfg.AddProfile(new ViewModelToDomainMappingProfile());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}