using Hackaton.Application.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackaton.Application.Interfaces.Services
{
    public interface IServiceManager
    {
        IEmployeeService EmployeeService { get; }
    }
}
