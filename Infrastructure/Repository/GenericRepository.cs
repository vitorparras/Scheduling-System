using Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DxContext _dbContext;
        public readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DxContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) => await _dbSet.Where(predicate).ToListAsync();

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => await _dbSet.Where(predicate).FirstOrDefaultAsync();

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();


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

        public async Task RemoveAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<TEntity> FromSqlRaw(string sql)
        {
            return await _dbSet.FromSqlRaw(sql).FirstOrDefaultAsync();
        }
    }
}
