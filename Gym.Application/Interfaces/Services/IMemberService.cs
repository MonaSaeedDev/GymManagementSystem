using Gym.Application.DTOs.Common;
using Gym.Application.DTOs.Members;

namespace Gym.Application.Interfaces.Services;

 public interface IMemberService
{
    Task<MemberResponse> CreateAsync(CreateMemberRequest request, CancellationToken ct = default);
    Task<MemberResponse> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<MemberListItem>> ListAsync(CancellationToken ct = default);
    Task<PagedResponse<MemberListItem>> ListPagedAsync(PagedRequest request, CancellationToken ct);
}
