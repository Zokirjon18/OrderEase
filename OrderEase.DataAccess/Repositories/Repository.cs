using Microsoft.EntityFrameworkCore;
using OrderEase.DataAccess.Contexts;
using OrderEase.Domain.Entitites;

namespace OrderEase.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
            _context.Set<TEntity>();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            var createdEntity = (await _context.AddAsync(entity)).Entity;
            await _context.SaveChangesAsync();

            return createdEntity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            entity.DeletedAt = DateTime.UtcNow;
            entity.IsDeleted = true;

            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> SelectAsync(long id)
        {
           return await _context.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public IQueryable<TEntity> SelectAllAsQueryable()
        {
            return _context.Set<TEntity>().AsQueryable();
        }
    }
}
