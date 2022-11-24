using Maintenance.Case.Application.ViewModels.Entities;


namespace Unidas.MS.Maintenance.Case.Application.ViewModels.MaintenanceCase
{
    public class CaseSalesforce
    {
        public string CarWorkshopScheduleDateTime { get; set; }
        public List<Category> Categories { get; set; }

        public string Comment { get; set; }

        public string CreationDateTime { get; set; }

        public Driver Driver { get; set; }

        public int Odometer { get; set; }

        public string Plate { get; set; }

        public string RequestDescription { get; set; }

        public string ServiceOrderNumber { get; set; }

        public string SupplierCnpj { get; set; }

        public string Status { get; set; }

        public string City { get; set; }

        public string State { get; set; }
    }
}
