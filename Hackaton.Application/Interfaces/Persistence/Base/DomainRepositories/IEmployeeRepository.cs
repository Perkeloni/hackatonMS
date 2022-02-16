using Hackaton.Application.DataTransferObject;
using Hackaton.Domain.Entities;

namespace Hackaton.Application.Interfaces.Persistence.Base.DomainRepositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken = default, bool trackChanges = false);
        Task<Employee> GetByIdAsync(Guid employeeId, CancellationToken cancellationToken = default, bool trackChanges = false);
        Task InsertAsync(Employee employee, CancellationToken cancellationToken = default);
        void Remove(Employee employee);
    }
}