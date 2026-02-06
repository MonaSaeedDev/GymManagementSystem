using Gym.Application.Interfaces;
using Gym.Application.Interfaces.Repositories;
using Gym.Domain.Entities;
using Gym.Infrastructure.Data;
using Gym.Infrastructure.Repositories;

namespace Gym.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly GymDbContext _context;
    public IGenericReository<Member> Members { get; }
    public IGenericReository<Trainer> Trainers { get; }
    public IGenericReository<Session> Sessions { get; }
    public IGenericReository<Booking> Bookings { get; }
    public IGenericReository<MembershipPlan> MembershipPlans { get; }
    public UnitOfWork(GymDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));

        Members = new GenericRepository<Member>(_context);
        Trainers = new GenericRepository<Trainer>(_context);
        Sessions = new GenericRepository<Session>(_context);
        Bookings = new GenericRepository<Booking>(_context);
        MembershipPlans = new GenericRepository<MembershipPlan>(_context);
    }

    public Task<int> SaveChangesAsync(CancellationToken ct = default)
    => _context.SaveChangesAsync(ct);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
}
