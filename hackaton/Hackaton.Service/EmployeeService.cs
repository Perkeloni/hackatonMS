using AutoMapper;
using Hackaton.Application.DataTransferObject;
using Hackaton.Application.Interfaces.Persistence;
using Hackaton.Application.Interfaces.Services;
using Hackaton.Domain.Entities;
using Hackaton.Domain.Exceptions.NotFoundException;

namespace Hackaton.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public EmployeeService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        private async Task<Employee> GetEmployeeById(Guid employeeId, CancellationToken cancellationToken, bool trackChanges = false)
        {
            var employeeFromDb = await _repositoryManager.Employee.GetByIdAsync(employeeId, cancellationToken, trackChanges);
            if (employeeFromDb is null)
            {
                throw new EmployeeNotFoundException(employeeId);
            }
            return employeeFromDb;
        }

        public async Task<EmployeeDto> CreateAsync(EmployeeForCreationDto employeeForCreation, CancellationToken cancellationToken = default)
        {
            var employee = _mapper.Map<Employee>(employeeForCreation);

            await _repositoryManager.Employee.InsertAsync(employee, cancellationToken);
            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
            EmployeeDto result = new EmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Address = employee.Address.Line1,
                NIF = employee.NIF,
                OldId = employee.OldId,
                Email = employee.Email,
            };
            return (result);
        }

        public async Task DeleteAsync(Guid employeeId, CancellationToken cancellationToken)
        {
            var employeeFromDb = await GetEmployeeById(employeeId, cancellationToken);
            _repositoryManager.Employee.Remove(employeeFromDb);

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync(CancellationToken cancellationToken = default, bool trackChanges = false)
        {
            var employees = await _repositoryManager.Employee.GetAllAsync(cancellationToken, trackChanges);
            List<EmployeeDto> employeeDtos = new List<EmployeeDto>();
            foreach (Employee employee in employees)
            {
                employeeDtos.Add(new EmployeeDto
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Address = employee.Address?.Line1,
                    Email = employee.Email,
                    NIF = employee.NIF,
                    OldId = employee.OldId
                });
            }
            return employeeDtos;
        }

        public async Task<EmployeeDto> GetByIdAsync(Guid employeeId, CancellationToken cancellationToken = default, bool trackChanges = false)
        {
            var employeeFromDb = await GetEmployeeById(employeeId, cancellationToken);
            return new EmployeeDto
            {
                Id = employeeFromDb.Id,
                Name = employeeFromDb.Name,
                Address = employeeFromDb.Address?.Line1,
                Email = employeeFromDb.Email,
                NIF = employeeFromDb.NIF,
                OldId = employeeFromDb.OldId
            };
        }

        public async Task UpdateAsync(Guid employeeId, EmployeeForUpdateDto employeeForUpdate, CancellationToken cancellationToken = default)
        {
            Employee employeeFromDb = await GetEmployeeById(employeeId, cancellationToken, true);
            employeeFromDb.Name = employeeForUpdate.Name;
            employeeFromDb.Email = employeeForUpdate?.Email;
            employeeFromDb.NIF = employeeForUpdate?.NIF;

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }
    }
}