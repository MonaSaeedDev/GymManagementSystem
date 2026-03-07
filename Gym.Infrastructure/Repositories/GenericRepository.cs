using Gym.Application.Interfaces.Repositories;
using Gym.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Gym.Infrastructure.Repositories;

public class GenericRepository<TEntity> : IGenericReository<TEntity> 
    where TEntity : class
{
    protected readonly GymDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;
    
    public GenericRepository(GymDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<TEntity>();
    }
   
    public Task AddAsync(TEntity entity, CancellationToken ct = default)
    => _dbSet.AddAsync(entity, ct).AsTask();
    public Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default)
    => _dbSet.FindAsync(id, ct).AsTask();

    public async Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
    {
        var list = await _dbSet.AsNoTracking()
                             .Where(predicate)
                             .ToListAsync(ct);
        return list.AsReadOnly();
    }
    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
   => await _dbSet.AsNoTracking().ToListAsync(ct);
    public void Remove(TEntity entity) => _dbSet.Remove(entity);
    public void Update(TEntity entity)
    {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task<(IReadOnlyList<TEntity> Items, int TotalCount)> GetPagedAsync(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
        int page,
        int pageSize,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken ct = default)
    {
        var query = _dbSet.AsNoTracking();

        if (predicate is not null)
            query = query.Where(predicate);

        var totalCount = await query.CountAsync(ct);

                query = orderBy(query);

            var items = await query.Skip((page - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync(ct);

            return (items.AsReadOnly(), totalCount);
    }
}