using Gym.Application.Interfaces.Repositories;
using Gym.Domain.Common;
using Gym.Domain.Entities;

namespace Gym.Application.Interfaces.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Member> Members { get; }
    IGenericRepository<Trainer> Trainers { get; }
    IGenericRepository<Session> Sessions { get; }
    IGenericRepository<Booking> Bookings { get; }
    IGenericRepository<MembershipPlan> MembershipPlans { get; }

    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
