using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

ServiceBusClient serviceBusClient = new ServiceBusClient(configuration["ServiceBus:ConnectionString"]);
ServiceBusProcessor processor = serviceBusClient.CreateProcessor(configuration["ServiceBus:Queue"], new ServiceBusProcessorOptions());

try
{
    processor.ProcessMessageAsync += MessageHandler;
    processor.ProcessErrorAsync += ErrorHandler;

    await processor.StartProcessingAsync();

    Console.WriteLine("Maintenance Case - Processing messages");
    Console.ReadKey();

   
}
finally
{
    await processor.DisposeAsync();
    await serviceBusClient.DisposeAsync();
}

async Task MessageHandler(ProcessMessageEventArgs args)
{
    string body = args.Message.Body.ToString();
    Console.WriteLine($"Received: {body}");

    // Logica do problema

    await args.CompleteMessageAsync(args.Message);
}

Task ErrorHandler(ProcessErrorEventArgs args)
{
    Console.WriteLine(args.Exception.ToString());
    return Task.CompletedTask;
}