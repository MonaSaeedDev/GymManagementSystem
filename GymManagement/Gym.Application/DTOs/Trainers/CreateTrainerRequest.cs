namespace Gym.Application.DTOs.Trainers;

public sealed record CreateTrainerRequest(
    string FullName,
    string Specialty
);
