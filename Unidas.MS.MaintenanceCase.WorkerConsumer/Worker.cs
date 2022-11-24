using Azure.Messaging.ServiceBus;
using Unidas.MS.Maintenance.Case.Application.ViewModels;
using Newtonsoft.Json;
using Unidas.MS.Maintenance.Case.Application.ViewModels.Requests;
using Unidas.MS.Maintenance.Case.Application.Interfaces.Services.UseCases;
using Unidas.MS.Maintenance.Case.Application.Interfaces.Services.Case;
using Azure.Core;

namespace Unidas.MS.Maintenance.Case.WorkerConsumer
{
    public abstract class Worker : BackgroundService
    {
        protected readonly ILogger<Worker> _logger;
        //protected readonly ICheckinCheckoutService _checkinCheckoutService;
        protected readonly AppSettings _appSettings;
        protected ServiceBusProcessor _processor;
        protected ServiceBusClient _serviceBusClient;
        protected ITokenSalesforceService _tokenSalesforceService;
        protected IMaintenanceCaseServiceSalesforce _maintenanceCaseServiceSalesforce;
        protected IMaintenanceCaseServiceJira _maintenanceCaseServiceJira;
        protected ServiceBusClientOptions _serviceBusClientOptions;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory factory)
        {
            _logger = logger;
            //_checkinCheckoutService = factory.CreateScope().ServiceProvider.GetRequiredService<ICheckinCheckoutService>();
            _tokenSalesforceService = factory.CreateScope().ServiceProvider.GetRequiredService<ITokenSalesforceService>();
            _maintenanceCaseServiceSalesforce = factory.CreateScope().ServiceProvider.GetRequiredService<IMaintenanceCaseServiceSalesforce>();
            _maintenanceCaseServiceJira = factory.CreateScope().ServiceProvider.GetRequiredService<IMaintenanceCaseServiceJira>();

            _appSettings = factory.CreateScope().ServiceProvider.GetRequiredService<AppSettings>();
            ServiceBusClientOptions _serviceBusClientOptions = new ServiceBusClientOptions();
            _serviceBusClientOptions.RetryOptions = new ServiceBusRetryOptions
            {
                Delay = TimeSpan.FromSeconds(5),
                MaxDelay = TimeSpan.FromSeconds(30),
                Mode = ServiceBusRetryMode.Exponential,
                MaxRetries = 10
            };

            _serviceBusClient = new ServiceBusClient(_appSettings.ServiceBusSettings.PrimaryConnectionString);
            _processor = _serviceBusClient.CreateProcessor(_appSettings.ServiceBusSettings.QueueName, new ServiceBusProcessorOptions());
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _processor.ProcessMessageAsync += MessageHandler;
                _processor.ProcessErrorAsync += ErrorHandler;

                await _processor.StartProcessingAsync();

                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }

                await _processor.CloseAsync(cancellationToken: stoppingToken);
            }
            finally
            {
                await _processor.DisposeAsync();
                await _serviceBusClient.DisposeAsync();
            }
        }

        async Task MessageHandler(ProcessMessageEventArgs args)
        {
            try
            {
                var body = args.Message.Body.ToString();               

                var request = JsonConvert.DeserializeObject<ItemMaintenanceCaseRequestViewModel>(body);
                                
                if (request == null)
                {
                    _logger.LogError("Unable to deserialize message: {0}", body);
                    return;
                }

                if (await ProcessMessage(request, args.CancellationToken) == true)
                {
                    await args.CompleteMessageAsync(args.Message);                    
                } else
                {
                    await args.AbandonMessageAsync(args.Message);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            _logger.LogError("Erro no processamento: {0}", args.Exception.Message.ToString());
            return Task.CompletedTask;
        }

        protected abstract Task<bool> ProcessMessage(ItemMaintenanceCaseRequestViewModel request, CancellationToken cancellationToken);
    }
}