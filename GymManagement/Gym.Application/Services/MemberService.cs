using Gym.Application.DTOs.Members;
using Gym.Application.Exceptions;
using Gym.Application.Interfaces.Services;
using Gym.Application.Interfaces.UnitOfWork;
using Gym.Domain.Entities;

namespace Gym.Application.Services;

public sealed class MemberService : IMemberService
{
    private readonly IUnitOfWork _uow;

    public MemberService(IUnitOfWork uow) => _uow = uow;

    public async Task<MemberResponse> CreateAsync(CreateMemberRequest request, CancellationToken ct = default)
    {
        var plan = await _uow.MembershipPlans.GetByIdAsync(request.MembershipPlanId, ct);
        if (plan is null)
            throw new NotFoundException($"MembershipPlan with id {request.MembershipPlanId} was not found.");

        var member = new Member(
            fullName: request.FullName,
            phone: request.Phone,
            email: request.Email,
            startDate: request.MembershipStartDate,
            endDate: request.MembershipEndDate,
            membershipPlanId: request.MembershipPlanId
        );

        await _uow.Members.AddAsync(member, ct);
        await _uow.SaveChangesAsync(ct);

        return MapToResponse(member, planName: plan.Name);
    }

    public async Task<MemberResponse> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var member = await _uow.Members.GetByIdAsync(id, ct);
        if (member is null)
            throw new NotFoundException($"Member with id {id} was not found.");

        var plan = await _uow.MembershipPlans.GetByIdAsync(member.MembershipPlanId, ct);
        var planName = plan?.Name ?? string.Empty;

        return MapToResponse(member, planName);
    }

    public async Task<IReadOnlyList<MemberListItem>> ListAsync(CancellationToken ct = default)
    {
        var members = await _uow.Members.ListAsync(ct);

        return members
            .Select(m => new MemberListItem(
                Id: m.Id,
                FullName: m.FullName,
                Phone: m.Phone,
                Status: m.Status.ToString()
            ))
            .ToList();
    }

    private static MemberResponse MapToResponse(Member member, string planName)
        => new(
            Id: member.Id,
            FullName: member.FullName,
            Phone: member.Phone,
            Email: member.Email,
            MembershipStartDate: member.MembershipStartDate,
            MembershipEndDate: member.MembershipEndDate,
            MembershipPlanId: member.MembershipPlanId,
            MembershipPlanName: planName,
            Status: member.Status.ToString(),
            CreatedAt: member.CreatedAt,
            UpdatedAt: member.UpdatedAt
        );
}
