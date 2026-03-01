using Gym.Application.DTOs.Sessions;

namespace Gym.Application.Interfaces.Services;

public interface ISessionService
{
    Task<SessionResponse> CreateAsync(CreateSessionRequest request, CancellationToken ct = default);
    Task<SessionResponse> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<SessionListItem>> ListAsync(CancellationToken ct = default);
}
