using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceCase.Domain.Models.Cases
{
    public sealed class MaintenanceCase : IEntity, IAggregateRoot
    {
        public MaintenanceCase(
            Guid id, 
            string idSalesForceQueue, 
            string idSalesForceSolicitacao, 
            string idSalesForceChild, 
            string caseNumber, 
            DateTime createdDateTime, 
            DateTime? modifiedDateTime, 
            bool isDealer, 
            DateTime? scheduledDateTime, 
            string registrationNumber, 
            string driverCPF, 
            string description, 
            int hodometer, 
            string status, 
            string cancelReason, 
            bool sendCancel, 
            bool sendReSchedule, 
            string caseTempNumber, 
            string cnpj, 
            bool isWinche
            )
        {
            Id = id;
            IdSalesForceQueue = idSalesForceQueue;
            IdSalesForceSolicitacao = idSalesForceSolicitacao;
            IdSalesForceChild = idSalesForceChild;
            CaseNumber = caseNumber;
            CreatedDateTime = createdDateTime;
            ModifiedDateTime = modifiedDateTime;
            IsDealer = isDealer;
            ScheduledDateTime = scheduledDateTime;
            RegistrationNumber = registrationNumber;
            DriverCPF = driverCPF;
            Description = description;
            Hodometer = hodometer;
            Status = status;
            CancelReason = cancelReason;
            SendCancel = sendCancel;
            SendReSchedule = sendReSchedule;
            CaseTempNumber = caseTempNumber;
            Cnpj = cnpj;
            IsWinche = isWinche;
        }

        public Guid Id { get; private set; }

        public string IdSalesForceQueue { get; set; }
        public string IdSalesForceSolicitacao { get; set; }
        public string IdSalesForceChild { get; set; }
        public string CaseNumber { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public bool IsDealer { get; set; }
        public DateTime? ScheduledDateTime { get; set; }
        public string RegistrationNumber { get; set; }
        public string DriverCPF { get; set; }
        public string Description { get; set; }
        public int Hodometer { get; set; }
        public string Status { get; set; }
        public string CancelReason { get; set; }
        public bool SendCancel { get; set; }
        public bool SendReSchedule { get; set; }
        public string CaseTempNumber { get; set; }
        public string Cnpj { get; set; }
        public bool IsWinche { get; set; }

    }
}
