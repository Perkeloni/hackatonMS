using Hackaton.Application.Common.Models;
using Hackaton.Application.DataTransferObject;
using Hackaton.Application.Models.Employee;
using Hackaton.Domain.Entities;
using System.Dynamic;

namespace Hackaton.Application.Interfaces.Persistence.Base.DomainRepositories
{
    public interface IEmployeeRepository
    {
        Task<PagedList<ExpandoObject>> GetAllAsync(EmployeeParameters employeeParameters, CancellationToken cancellationToken = default, bool trackChanges = false);
        Task<Employee> GetByIdAsync(Guid employeeId, CancellationToken cancellationToken = default, bool trackChanges = false);
        Task InsertAsync(Employee employee, CancellationToken cancellationToken = default);
        void Remove(Employee employee);
    }
}