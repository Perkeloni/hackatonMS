namespace Hackaton.Application.Interfaces.Persistence.Helper
{
    public interface ISortHelper<T>
    {
        IQueryable<T> ApplySort(IQueryable<T> entities, string orderByQueryString);
    }
}