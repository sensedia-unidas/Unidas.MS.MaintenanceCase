using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Case.Application.ViewModels.Entities
{
    public class Driver
    {
        public Driver(string cellPhone, string cpf, string driverLicenseNumber, string driverLicenseDueDateTime, string email, string fleetManager, string fullName, string plate, string rg, Account account)
        {
            CellPhone = cellPhone;
            Cpf = cpf;
            DriverLicenseNumber = driverLicenseNumber;
            DriverLicenseDueDateTime = driverLicenseDueDateTime;
            Email = email;
            FleetManager = fleetManager;
            FullName = fullName;
            Plate = plate;
            Rg = rg;
            Account = account;
        }

        public string CellPhone { get; set; }
        public string Cpf { get; set; }

        public string DriverLicenseNumber { get; set; }

        public string DriverLicenseDueDateTime { get; set; }

        public string Email { get; set; }

        public string FleetManager { get; set; }

        public string FullName { get; set; }

        public string Plate { get; set; }

        public string Rg { get; set; }

        public Account Account { get; set; }
    }
}
