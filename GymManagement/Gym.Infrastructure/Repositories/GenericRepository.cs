using Gym.Application.Interfaces.Repositories;
using Gym.Domain.Common;
using Gym.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Gym.Infrastructure.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly GymDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public GenericRepository(GymDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default)
        => _dbSet.FindAsync(id, ct).AsTask();

    public async Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken ct = default)
        => await _dbSet.AsNoTracking().ToListAsync(ct);

    public Task AddAsync(TEntity entity, CancellationToken ct = default)
        => _dbSet.AddAsync(entity, ct).AsTask();

    public void Update(TEntity entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(TEntity entity)
        => _dbSet.Remove(entity);
}
