using Hackaton.Application.Interfaces.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hackaton.Persistence
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly RepositoryContext _repositoryContext;
        protected RepositoryBase(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public IQueryable<T> FindAll(CancellationToken cancellationToken, bool trackChanges)
            => !trackChanges ? _repositoryContext.Set<T>().AsNoTracking() : _repositoryContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, CancellationToken cancellationToken, bool trackChanges)
            => !trackChanges ? _repositoryContext.Set<T>().Where(expression).AsNoTracking() : _repositoryContext.Set<T>().Where(expression);

        public async Task Insert(T entity, CancellationToken cancellationToken)
            => await _repositoryContext.Set<T>().AddAsync(entity, cancellationToken);

        public void Remove(T entity)
            => _repositoryContext.Set<T>().Remove(entity);
    }
}