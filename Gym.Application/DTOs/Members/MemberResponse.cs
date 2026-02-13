namespace Gym.Application.DTOs.Members;

public sealed record MemberResponse(
     int Id,
    string FullName,
    string Phone,
    string Email,
    DateOnly MembershipStartDate,
    DateOnly MembershipEndDate,
    int MembershipPlanId,
    string MembershipPlanName,
    string Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt
    );
