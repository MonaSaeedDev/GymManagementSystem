using Gym.Application.Interfaces.Repositories;
using Gym.Domain.Entities;

namespace Gym.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericReository<Member> Members { get; }
    IGenericReository<Trainer> Trainers { get; }
    IGenericReository<Session> Sessions { get; }
    IGenericReository<Booking> Bookings { get; }
    IGenericReository<MembershipPlan> MembershipPlans { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
