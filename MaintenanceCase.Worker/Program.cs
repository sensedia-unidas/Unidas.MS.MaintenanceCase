
using MaintenanceCase.Application.Interfaces.Services;
using MaintenanceCase.Application.Services;
using Unidas.MS.Maintenance.Application.Interfaces.Services;

namespace MaintenanceCase.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
      
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
                })
                .ConfigureServices((hostContext, services) =>
                {

                    //var appSettingsConfig = hostContext.Configuration.GetSection(nameof(AppSettings));
                    services.AddHostedService<Worker>()
                        .AddScoped<IMaintenanceCaseService, MaintenanceCaseService>();
                    
                })
                .Build();

            host.Run();
        }
    }
}
