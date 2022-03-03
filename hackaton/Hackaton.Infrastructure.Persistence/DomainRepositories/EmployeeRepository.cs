using Hackaton.Application.Common.Models;
using Hackaton.Application.DataTransferObject;
using Hackaton.Application.Interfaces.Persistence.Base.DomainRepositories;
using Hackaton.Application.Models.Employee;
using Hackaton.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using Hackaton.Application.Interfaces.Persistence.Helper;
using System.Dynamic;

namespace Hackaton.Persistence.DomainRepositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        private readonly ISortHelper<Employee> _sortHelper;
        private readonly IDataShaper<Employee> _dataShaper;
        public EmployeeRepository(RepositoryContext repositoryContext, ISortHelper<Employee> sortHelper, IDataShaper<Employee> dataShaper) : base(repositoryContext)
        {
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }
        private static Expression<Func<Employee, bool>> GetFilterEmployees(EmployeeParameters employeeParameters) => e
            =>  e.Name.Contains(employeeParameters.ContainsNameFilter) &&
                (string.IsNullOrEmpty(employeeParameters.EmailFilter) ||
                e.Email.Equals(employeeParameters.EmailFilter));

        public async Task<PagedList<ExpandoObject>> GetAllAsync(EmployeeParameters employeeParameters, CancellationToken cancellationToken = default, bool trackChanges = false)
        {
            var employees = FindByCondition(GetFilterEmployees(employeeParameters), cancellationToken, trackChanges);

            var sortedEmployees = await _sortHelper.ApplySort(employees, employeeParameters.OrderBy)
                .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
                .Take(employeeParameters.PageSize)
                .ToListAsync(cancellationToken);

            var shapedEmployees = _dataShaper.ShapeData(sortedEmployees, employeeParameters.Fields).ToList();

            var count = await FindByCondition(GetFilterEmployees(employeeParameters), cancellationToken, trackChanges)
                        .CountAsync(cancellationToken);


            //var employees = await FindAll(cancellationToken, trackChanges)
            //    .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
            //    .Take(employeeParameters.PageSize)
            //    .ToListAsync(cancellationToken);

            return PagedList<ExpandoObject>.ToPageList(shapedEmployees, count, employeeParameters.PageNumber, employeeParameters.PageSize);
        }

        public async Task<Employee> GetByIdAsync(Guid employeeId, CancellationToken cancellationToken = default, bool trackChanges = false)
            => await FindByCondition(e => e.Id.Equals(employeeId), cancellationToken, trackChanges).SingleOrDefaultAsync();

        public async Task InsertAsync(Employee employee, CancellationToken cancellationToken = default)
            => await Insert(employee, cancellationToken);

        public new void Remove(Employee employee)
            => base.Remove(employee);
    }
}