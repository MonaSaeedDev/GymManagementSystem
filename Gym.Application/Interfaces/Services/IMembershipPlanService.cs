using Gym.Application.DTOs.MembershipPlans;

namespace Gym.Application.Interfaces.Services;

public interface IMembershipPlanService
{
    Task<MembershipPlanResponse> CreateAsync(CreateMembershipPlanRequest request, CancellationToken ct = default);
    Task<MembershipPlanResponse> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<MembershipPlanListItem>> ListAsync(CancellationToken ct = default);
}
