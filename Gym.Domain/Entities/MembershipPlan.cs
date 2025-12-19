using Gym.Domain.Common;

namespace Gym.Domain.Entities;

public class MembershipPlan : BaseEntity
{
    public string Name { get; private set; } = null!;
    public decimal Price { get; private set; }
    public int MaxSessionsPerMonth { get; private set; }

    private MembershipPlan() { }
    public MembershipPlan(string name, decimal price, int maxSessionsPerMonth) =>
        (Name, Price, MaxSessionsPerMonth) = (name, price, maxSessionsPerMonth);
}