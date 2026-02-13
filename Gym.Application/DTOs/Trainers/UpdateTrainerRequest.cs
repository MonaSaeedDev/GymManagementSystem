namespace Gym.Application.DTOs.Trainers;

public sealed record UpdateTrainerRequest(
    string FullName,
    string Specialty
);
