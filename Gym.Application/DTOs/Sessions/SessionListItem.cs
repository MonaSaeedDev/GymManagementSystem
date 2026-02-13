namespace Gym.Application.DTOs.Sessions;

public sealed record SessionListItem(
    int Id,
    string Title,
    DateOnly Date,
    TimeOnly StartTime,
    string Status,
    int Capacity
);
