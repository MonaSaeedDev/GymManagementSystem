namespace Gym.Application.DTOs.Members;

public sealed record CreateMemberRequest(
    string FullName,
    string Phone,
    string Email,
    DateOnly MembershipStartDate,
    DateOnly MembershipEndDate,
    int MembershipPlanId
);
