using Gym.Domain.Common;
using Gym.Domain.Enums;

namespace Gym.Domain.Entities;

public class Member : BaseEntity
{
    public string FullName { get; private set; } = null!;
    public string Phone { get; private set; } = null!;
    public string Email { get; private set; } = null!;

    public DateOnly MembershipStartDate { get; private set; }
    public DateOnly MembershipEndDate { get; private set; }
    public MembershipStatus Status { get; private set; }

    public int MembershipPlanId { get; private set; }
    public MembershipPlan MembershipPlan { get; private set; } = null!;

    private readonly List<Booking> _bookings = new();
    public IReadOnlyCollection<Booking> Bookings => _bookings;

    private Member() { }

    public Member(string fullName, string phone, string email, DateOnly startDate, DateOnly endDate, int membershipPlanId)
        => (FullName, Phone, Email, MembershipStartDate, MembershipEndDate, MembershipPlanId, Status)
         = (fullName, phone, email, startDate, endDate, membershipPlanId, MembershipStatus.Active);

    public void ExpireMembership()
    {
        Status = MembershipStatus.Expired;
        SetUpdated();
    }
}
