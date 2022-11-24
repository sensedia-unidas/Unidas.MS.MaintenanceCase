using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Unidas.MS.Maintenance.Application.Interfaces.Services;
using Unidas.MS.Maintenance.Case.Application.ViewModels.Requests;

namespace MaintenanceCase.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private ServiceBusProcessor processor;
        private IMaintenanceCaseService _maintenanceCaseService;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IMaintenanceCaseService maintenanceCaseService)
        {
            _logger = logger;
            _configuration = configuration;
            _maintenanceCaseService = maintenanceCaseService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _logger.LogInformation("Maintenance Case Services - Prossessing Messages: {time}", DateTimeOffset.Now);

            // Create a ServiceBusClient that will authenticate using a connection string
            string connectionString = _configuration.GetValue<string>("AppSettings:ServiceBusSettings:PrimaryConnectionString");
            string queueName = _configuration.GetValue<string>("AppSettings:ServiceBusSettings:QueueName");
            

            ServiceBusClient client = new ServiceBusClient(connectionString);

            processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

            try
            {
                // add handler to process messages
                processor.ProcessMessageAsync += MessageHandler;

                // add handler to process any errors
                processor.ProcessErrorAsync += ErrorHandler;

                // start processing 
                await processor.StartProcessingAsync();

                Console.WriteLine("Wait for a minute and then press any key to end the processing");
                Console.ReadKey();

                // stop processing 
                Console.WriteLine("\nStopping the receiver...");
                await processor.StopProcessingAsync();
                Console.WriteLine("Stopped receiving messages");
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await processor.DisposeAsync();
                await client.DisposeAsync();
            }

        }

        // handle received messages
        async Task MessageHandler(ProcessMessageEventArgs args)
        {
            
            
            try
            {
                string body = args.Message.Body.ToString();
                string dateFormat = "dd/MM/yyyy HH:mm";
                var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = dateFormat };
                var request = JsonConvert.DeserializeObject<ItemMaintenanceCaseRequestViewModel>(body, dateTimeConverter);

                if(request != null)
                {
                    var result = await _maintenanceCaseService.Integrate(request);

                    await args.CompleteMessageAsync(args.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro", ex);
                throw;
            }
            
            // MaintenanceCaseRequestViewModel maintenanceCase = JsonConvert.DeserializeObject<MaintenanceCaseRequestViewModel>(body);
            //_logger.LogInformation("Processing message from " + result.Driver.Cpf);
            //_logger.LogInformation(body);

            //_logger.LogInformation("Message processed with success");


            // complete the message. message is deleted from the queue. 
           
        }

        // handle any errors when receiving messages
        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}