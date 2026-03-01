namespace Gym.Application.DTOs.Members;

public sealed record UpdateMemberRequest(
    string FullName,
    string Phone,
    string Email,
    DateOnly MembershipStartDate,
    DateOnly MembershipEndDate,
    int MembershipPlanId
);
