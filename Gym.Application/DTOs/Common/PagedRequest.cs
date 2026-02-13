namespace Gym.Application.DTOs.Common;

public sealed record PagedRequest(
    int Page = 1,
    int PageSize = 20,
    string? Search = null
);
