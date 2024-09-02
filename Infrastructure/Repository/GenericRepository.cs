using Domain.Model.Bases;
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

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) => 
            await _dbSet.AnyAsync(predicate, cancellationToken);

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await _dbSet.ToListAsync(cancellationToken);

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) => 
            await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);

        public async Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) =>
            await _dbSet.Where(predicate).ToListAsync(cancellationToken);

        public async Task<TEntity?> FindAsync(object[] keyValues, CancellationToken cancellationToken = default) =>
            await _dbSet.FindAsync(keyValues, cancellationToken);

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) =>
            await _dbSet.CountAsync(predicate, cancellationToken);

        public async Task<IEnumerable<TEntity>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default) =>
            await _dbSet.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

        public async Task UpdatePartialAsync(TEntity entity, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] updatedProperties)
        {
            var entry = _dbContext.Entry(entity);
            _dbSet.Attach(entity);

            foreach (var property in updatedProperties)
            {
                entry.Property(property).IsModified = true;
            }

            await SaveChangesAsync(cancellationToken);
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
            await SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<bool> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
            return await SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;

            foreach (var changedEntity in _dbContext.ChangeTracker.Entries())
            {
                if (changedEntity.Entity is BaseEntity entity)
                {
                    if (changedEntity.State == EntityState.Added)
                    {
                        entity.CreatedAt = now;
                        entity.UpdatedAt = now;
                    }
                    else if (changedEntity.State == EntityState.Modified)
                    {
                        entity.UpdatedAt = now;
                    }
                }
            }

            if (_dbContext.ChangeTracker.HasChanges())
            {
                return await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return 0;
        }
    }
}
