using Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly SchedulerContext _dbContext;
        public readonly DbSet<TEntity> _dbSet;

        public GenericRepository(SchedulerContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() =>
            await _dbSet.ToListAsync();

        public async Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate) =>
            await _dbSet.Where(predicate).ToListAsync();

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) =>
            await _dbSet.FirstOrDefaultAsync(predicate);

        public async Task<int> SaveChangesAsync() =>
            await _dbContext.SaveChangesAsync();

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task<bool> RemoveAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            return await SaveChangesAsync() > 0;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate) => await _dbSet.AnyAsync(predicate);



    }
}
