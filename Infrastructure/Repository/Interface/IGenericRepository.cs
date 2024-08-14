using System.Linq.Expressions;

namespace Infrastructure.Repository.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> RemoveAsync(TEntity entity);
        Task<int> SaveChangesAsync();
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
