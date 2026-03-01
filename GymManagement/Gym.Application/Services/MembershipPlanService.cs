using Gym.Application.DTOs.MembershipPlans;
using Gym.Application.Exceptions;
using Gym.Application.Interfaces.Services;
using Gym.Application.Interfaces.UnitOfWork;
using Gym.Domain.Entities;

namespace Gym.Application.Services;

public sealed class MembershipPlanService : IMembershipPlanService
{
    private readonly IUnitOfWork _uow;

    public MembershipPlanService(IUnitOfWork uow) => _uow = uow;

    public async Task<MembershipPlanResponse> CreateAsync(CreateMembershipPlanRequest request, CancellationToken ct = default)
    {
        var plan = new MembershipPlan(
            name: request.Name,
            price: request.Price,
            maxSessionsPerMonth: request.MaxSessionsPerMonth
        );

        await _uow.MembershipPlans.AddAsync(plan, ct);
        await _uow.SaveChangesAsync(ct);

        return MapToResponse(plan);
    }

    public async Task<MembershipPlanResponse> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var plan = await _uow.MembershipPlans.GetByIdAsync(id, ct);
        if (plan is null)
            throw new NotFoundException($"MembershipPlan with id {id} was not found.");

        return MapToResponse(plan);
    }

    public async Task<IReadOnlyList<MembershipPlanListItem>> ListAsync(CancellationToken ct = default)
    {
        var plans = await _uow.MembershipPlans.ListAsync(ct);

        return plans
            .Select(p => new MembershipPlanListItem(
                Id: p.Id,
                Name: p.Name,
                Price: p.Price,
                MaxSessionsPerMonth: p.MaxSessionsPerMonth
            ))
            .ToList();
    }

    private static MembershipPlanResponse MapToResponse(MembershipPlan plan)
        => new(
            Id: plan.Id,
            Name: plan.Name,
            Price: plan.Price,
            MaxSessionsPerMonth: plan.MaxSessionsPerMonth,
            CreatedAt: plan.CreatedAt,
            UpdatedAt: plan.UpdatedAt
        );
}
