using OrderEase.Domain.Entitites;

namespace OrderEase.DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : Auditable
    {
        Task<TEntity> InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<TEntity> SelectAsync(long id);
        IQueryable<TEntity> SelectAllAsQueryable();
    }
}
