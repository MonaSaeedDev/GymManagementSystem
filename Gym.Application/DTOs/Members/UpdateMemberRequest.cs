namespace Gym.Application.DTOs.Members;

public sealed record UpdateMemberRequest(
    string FullName,
    string Email,
    string Phone,
    DateOnly MembershipStartDate,
    DateOnly MembershipEndDate,
    int MembershipPlanId
    );
