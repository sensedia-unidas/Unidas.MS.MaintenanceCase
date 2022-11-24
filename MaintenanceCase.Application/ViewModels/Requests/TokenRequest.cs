using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidas.MS.Maintenance.Case.Application.ViewModels.Requests
{
    internal class TokenRequest
    {
        public string clientId { get; set; }
        public string clientSecret { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
    }
}
