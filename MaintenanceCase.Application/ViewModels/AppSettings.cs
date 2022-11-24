namespace Unidas.MS.Maintenance.Case.Application.ViewModels
{
    public class AppSettings
    {
        public string JiraIntegrationUrl { get; set; }
        public ServiceBusSettings ServiceBusSettings { get; set; } = new ServiceBusSettings();

        public SalesForce SalesForce { get; set; } = new SalesForce();
    }

    public class SalesForce
    {
        public string MSTokenUrl { get; set; }
        public string UrlCaseAppOnRoad { get; set; }
        public string clientId { get; set; }
        public string clientSecret { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
    }

    public class ServiceBusSettings
    {
        public string PrimaryConnectionString { get; set; }
        public string SecondConnectionString { get; set; }
        public string QueueName { get; set; }
        public string PrimaryKey { get; set; }
        public string SecondKey { get; set; }
        public string ArmId { get; set; }
    }
}