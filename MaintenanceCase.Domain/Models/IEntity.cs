using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceCase.Domain.Models
{
    internal interface IEntity
    {
        Guid Id { get; }
    }
}
