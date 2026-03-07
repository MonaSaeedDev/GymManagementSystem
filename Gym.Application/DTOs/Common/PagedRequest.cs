using System.ComponentModel.DataAnnotations;

namespace Gym.Application.DTOs.Common;

public enum SortDirection
{
    Asc = 0,
    Desc = 1
}

public sealed record PagedRequest(
    [param: Range(1, int.MaxValue)]
    int Page = 1,

    [param: Range(1, 200)]
    int PageSize = 20,

    [param: MaxLength(200)]
    string? Search = null, 

    [param: MaxLength(50)]
    string? SortBy = "id",

    SortDirection SortDir = SortDirection.Asc
);