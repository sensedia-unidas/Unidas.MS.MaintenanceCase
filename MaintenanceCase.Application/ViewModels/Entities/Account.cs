using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Case.Application.ViewModels.Entities
{
    public class Account
    {
        public Account(string password, string username)
        {
            Password = password;
            Username = username;
        }

        public string Password { get; set; }

        public string Username { get; set; }
    }
}
