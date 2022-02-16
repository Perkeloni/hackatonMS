using System.Linq.Expressions;

namespace Hackaton.Application.Interfaces.Persistence.Base
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(CancellationToken cancellationToken, bool trackChanges);
        Task Insert (T entity, CancellationToken cancellationToken);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, CancellationToken cancellationToken, bool trackChanges);
        void Remove(T entity);
    }
}