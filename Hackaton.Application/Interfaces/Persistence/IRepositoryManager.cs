using Hackaton.Application.Interfaces.Persistence.Base.DomainRepositories;

namespace Hackaton.Application.Interfaces.Persistence
{
    public interface IRepositoryManager
    {
        IEmployeeRepository Employee { get; }
        IUnitOfWork UnitOfWork { get; }
    }
}