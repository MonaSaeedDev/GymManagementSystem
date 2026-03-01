namespace Gym.Application.DTOs.Sessions;

public sealed record UpdateSessionRequest(
    string Title,
    DateOnly Date,
    TimeOnly StartTime,
    int Capacity,
    int TrainerId
);
