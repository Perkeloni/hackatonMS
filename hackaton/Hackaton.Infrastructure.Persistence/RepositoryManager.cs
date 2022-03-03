using Hackaton.Application.Interfaces.Persistence;
using Hackaton.Application.Interfaces.Persistence.Base.DomainRepositories;
using Hackaton.Application.Interfaces.Persistence.Helper;
using Hackaton.Domain.Entities;
using Hackaton.Persistence.DomainRepositories;

namespace Hackaton.Persistence
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IUnitOfWork> _unitOfWork;
        private readonly ISortHelper<Employee> _employeeSortHelper;
        private readonly IDataShaper<Employee> _employeeDataShaper;
        public RepositoryManager(RepositoryContext repositoryContext, ISortHelper<Employee> sortHelper, IDataShaper<Employee> employeeDataShaper)
        {
            _employeeSortHelper = sortHelper;
            _employeeDataShaper = employeeDataShaper;
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(repositoryContext, _employeeSortHelper, _employeeDataShaper));
            _unitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(repositoryContext));
        }
        public IEmployeeRepository Employee => _employeeRepository.Value;

        public IUnitOfWork UnitOfWork => _unitOfWork.Value;
    }
}