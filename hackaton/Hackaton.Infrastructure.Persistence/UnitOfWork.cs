using Hackaton.Application.Interfaces.Persistence;

namespace Hackaton.Persistence
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly RepositoryContext _repositoryContext;
        public UnitOfWork(RepositoryContext repositoryContext)
            => _repositoryContext = repositoryContext;
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _repositoryContext.SaveChangesAsync(cancellationToken);
    }
}