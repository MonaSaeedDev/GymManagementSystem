namespace Gym.Application.DTOs.Sessions;

public sealed record CreateSessionRequest(
    string Title,
    DateOnly Date,
    TimeOnly StartTime,
    int Capacity,
    int TrainerId
);
