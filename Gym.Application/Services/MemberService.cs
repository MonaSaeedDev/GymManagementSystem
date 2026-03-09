using Gym.Application.DTOs.Common;
using Gym.Application.DTOs.Members;
using Gym.Application.Exceptions;
using Gym.Application.Interfaces.Services;
using Gym.Application.Interfaces.UnitOfWork;
using Gym.Domain.Entities;
using System.Linq;
using System.Linq.Expressions;

namespace Gym.Application.Services
{
    public sealed class MemberService : IMemberService
    {
        private readonly IUnitOfWork _uow;

        public MemberService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<MemberResponse> CreateAsync(CreateMemberRequest request, CancellationToken ct = default)
        {
           var plan = await GetMembershipPlanOrThrowAsync(request.MembershipPlanId, ct);

            var originalEmail = request.Email;
            var normalizedEmail = originalEmail.Trim().ToLowerInvariant();
            await EnsureEmailIsUniqueAsync(originalEmail, normalizedEmail, ct);

            var member = new Member(
                fullName: request.FullName,
                email: normalizedEmail,
                phone: request.Phone,
                membershipStartDate: request.MembershipStartDate,
                membershipEndDate: request.MembershipEndDate,
                membershipPlan: plan
                );

            await _uow.Members.AddAsync(member);
            await _uow.SaveChangesAsync();

            return MapToResponse(member, planName: plan.Name);
        }

        public async Task EnsureEmailIsUniqueAsync(string originalEmail, string normalizedEmail, CancellationToken ct)
        { 
            var existing = await _uow.Members.FindAsync(m => m.Email == normalizedEmail, ct);

            if(existing is not null)
                    throw new ConflictException($"Email '{originalEmail}' is already used.");
        }

        private async Task<MembershipPlan> GetMembershipPlanOrThrowAsync(int membershipPlanId, CancellationToken ct)
        {
            var plan = await _uow.MembershipPlans.GetByIdAsync(membershipPlanId, ct);
            if (plan is null)
                throw new NotFoundException($"MembershipPlan with id {membershipPlanId} was not found.");
            return plan;
        }
        public async Task<MemberResponse> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var member = await _uow.Members.GetByIdAsync(id, ct);
            if (member is null)
                throw new NotFoundException($"Member with id {id} was not found.");

            var plan = await _uow.MembershipPlans.GetByIdAsync(member.MembershipPlanId);
            if (plan == null)
                throw new ArgumentNullException(nameof(plan));

            return MapToResponse(member, planName: plan.Name);
        }
        public async Task<IReadOnlyList<MemberListItem>> ListAsync(CancellationToken ct = default)
        {
            var members = await _uow.Members.GetAllAsync(ct);

            return members
                .Select(m => new MemberListItem(
                Id: m.Id,
                FullName: m.FullName,
                Phone: m.Phone,
                Status: m.Status.ToString()
            )).ToList();
        }
        public async Task<PagedResponse<MemberListItem>> ListPagedAsync(PagedRequest request, CancellationToken ct)
        {
            var term = request.Search?.Trim();

            Expression<Func<Member, bool>>? predicate = null;

            if (!string.IsNullOrWhiteSpace(term))
                predicate =
                    m => m.FullName.Contains(term) ||
                         m.Email.Contains(term) ||
                         m.Phone.Contains(term);

            var orderBy = BuildMemberOrderBy(request);

            var (members, totalCount) = await _uow.Members.GetPagedAsync(
                predicate: predicate,
                orderBy: orderBy,
                page: request.Page,
                pageSize: request.PageSize,
                ct: ct
                );

            var items = members.Select(m => new MemberListItem(
                Id: m.Id,
                FullName: m.FullName,
                Phone: m.Phone,
                Status: m.Status.ToString()
            )).ToList();

            return new PagedResponse<MemberListItem>
            (
                Items: items,
                Page: request.Page,
                PageSize: request.PageSize,
                TotalCount: totalCount
            );
        }

        private static Func<IQueryable<Member>, IOrderedQueryable<Member>> BuildMemberOrderBy(PagedRequest request)
        {
            var sortBy = request.SortBy?.Trim();
            var isDesc = request.SortDir == SortDirection.Desc;

            if (string.IsNullOrWhiteSpace(sortBy))
              return q => isDesc ? q.OrderByDescending(m => m.Id) : q.OrderBy(m => m.Id);

            return sortBy.ToLowerInvariant() switch
            {
                "id" => q => isDesc ? q.OrderByDescending(m => m.Id) : q.OrderBy(m => m.Id),

                "fullname" => q => isDesc ? q.OrderByDescending(m => m.FullName) : q.OrderBy(m => m.FullName),

                _ => q => isDesc ? q.OrderByDescending(m => m.Id) : q.OrderBy(m => m.Id)
            };
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
                UpdatedAt: member.UpdatedAt);
    }
}
