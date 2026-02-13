namespace Gym.Application.DTOs.MembershipPlans;

public sealed record MembershipPlanResponse(
    int Id,
    string Name,
    decimal Price,
    int MaxSessionsPerMonth,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
