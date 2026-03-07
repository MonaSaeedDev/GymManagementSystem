using System.ComponentModel.DataAnnotations;

namespace Gym.Application.DTOs.MembershipPlans;

public sealed record CreateMembershipPlanRequest(
    [param: Required, MaxLength(120)]
    string Name,
    
    [Range(typeof(decimal), "0.01", "1000000")]
    decimal Price,

    [param: Range(1, 200)]
    int MaxSessionsPerMonth
);
