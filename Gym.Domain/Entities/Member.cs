using Gym.Domain.Common;
using Gym.Domain.Enums;

namespace Gym.Domain.Entities;

public class Member : BaseEntity
{
    public string FullName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Phone { get; private set; } = null!;
    public DateOnly MembershipStartDate { get; private set; } 
    public DateOnly MembershipEndDate { get; private set; }
    public MembershipStatus Status { get; private set; }
    public int MembershipPlanId { get; private set; }
    public MembershipPlan MembershipPlan { get; private set; } = null!;

    private readonly List<Booking> _bookings = new(); 
    public IReadOnlyCollection<Booking> Bookings => _bookings;

    private Member() { }
    public Member(
     string fullName,
     string email,
     string phone,
     DateOnly membershipStartDate,
     DateOnly membershipEndDate,
     MembershipPlan membershipPlan)
    {
        FullName = fullName;
        Email = email;
        Phone = phone;
        MembershipStartDate = membershipStartDate;
        MembershipEndDate = membershipEndDate;
        MembershipPlan = membershipPlan;
        MembershipPlanId = membershipPlan.Id;
        Status = MembershipStatus.Active;
    }
    public void ExpireMembership() 
    {
        Status = MembershipStatus.Expired;
        SetUpdated();
    }
}