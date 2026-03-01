using System.Collections.Concurrent;
using Gym.Application.Interfaces.Repositories;
using Gym.Application.Interfaces.UnitOfWork;
using Gym.Domain.Common;
using Gym.Domain.Entities;
using Gym.Infrastructure.Data;
using Gym.Infrastructure.Repositories;

namespace Gym.Infrastructure.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly GymDbContext _context;
    private readonly ConcurrentDictionary<Type, object> _repositories = new();

    public UnitOfWork(GymDbContext context)
    {
        _context = context;

        Members = new GenericRepository<Member>(_context);
        Trainers = new GenericRepository<Trainer>(_context);
        Sessions = new GenericRepository<Session>(_context);
        Bookings = new GenericRepository<Booking>(_context);
        MembershipPlans = new GenericRepository<MembershipPlan>(_context);
    }

    public IGenericRepository<Member> Members { get; }
    public IGenericRepository<Trainer> Trainers { get; }
    public IGenericRepository<Session> Sessions { get; }
    public IGenericRepository<Booking> Bookings { get; }
    public IGenericRepository<MembershipPlan> MembershipPlans { get; }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        var type = typeof(TEntity);

        if (type == typeof(Member)) return (IGenericRepository<TEntity>)Members;
        if (type == typeof(Trainer)) return (IGenericRepository<TEntity>)Trainers;
        if (type == typeof(Session)) return (IGenericRepository<TEntity>)Sessions;
        if (type == typeof(Booking)) return (IGenericRepository<TEntity>)Bookings;
        if (type == typeof(MembershipPlan)) return (IGenericRepository<TEntity>)MembershipPlans;

        return (IGenericRepository<TEntity>)_repositories.GetOrAdd(type, _ => new GenericRepository<TEntity>(_context));
    }

    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => _context.SaveChangesAsync(ct);

    public void Dispose()
        => _context.Dispose();
}
