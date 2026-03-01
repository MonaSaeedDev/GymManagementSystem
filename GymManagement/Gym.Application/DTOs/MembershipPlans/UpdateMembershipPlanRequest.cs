namespace Gym.Application.DTOs.MembershipPlans;

public sealed record UpdateMembershipPlanRequest(
    string Name,
    decimal Price,
    int MaxSessionsPerMonth
);
