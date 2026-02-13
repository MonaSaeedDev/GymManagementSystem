namespace Gym.Application.DTOs.Sessions;

public sealed record SessionResponse(
    int Id,
    string Title,
    DateOnly Date,
    TimeOnly StartTime,
    int Capacity,
    int TrainerId,
    string TrainerName,
    string Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
