using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Case.Application.ViewModels.MaintenanceCase
{
    public class CaseJira
    {
        public string Placa { get; set; }
        public string Cpf { get; set; }

        public string Cnpj { get; set; }
        public string Km { get; set; }
        public string IssueType { get; set; }

        public List<CustomFields> CustomFields { get; set; }

    }


    public class CustomFields
    {
        public string CfId { get; set; }

        public string CfName { get; set; }

        public string CfValue { get; set; }

    }

}
