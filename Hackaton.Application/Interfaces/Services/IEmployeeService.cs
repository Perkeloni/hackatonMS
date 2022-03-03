using Hackaton.Application.Common.Models;
using Hackaton.Application.DataTransferObject;
using Hackaton.Application.Models.Employee;
using System.Dynamic;

namespace Hackaton.Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<(IEnumerable<ExpandoObject>, MetaData metaData)> GetAllAsync(EmployeeParameters employeeParameters, CancellationToken cancellationToken = default, bool trackChanges = false);
        Task<EmployeeDto> GetByIdAsync(Guid employeeId, CancellationToken cancellationToken = default, bool trackChanges = false);
        Task<EmployeeDto> CreateAsync(EmployeeForCreationDto employeeForCreation, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid employeeId, EmployeeForUpdateDto employeeForUpdate, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid employeeId, CancellationToken cancellationToken);
    }
}