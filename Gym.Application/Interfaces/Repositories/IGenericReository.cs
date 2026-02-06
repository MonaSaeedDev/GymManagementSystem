using System.Linq.Expressions;

namespace Gym.Application.Interfaces.Repositories;

public interface IGenericReository<TEntity> where TEntity : class
{
    public Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default);
    public Task<IReadOnlyList<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate, 
        CancellationToken ct = default);
    public Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default);
    public Task AddAsync(TEntity entity, CancellationToken ct = default);
    public void Update(TEntity entity);
    public void Remove(TEntity entity);
}

