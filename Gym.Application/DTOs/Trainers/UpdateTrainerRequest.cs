using System.ComponentModel.DataAnnotations;

namespace Gym.Application.DTOs.Trainers;

public sealed record UpdateTrainerRequest(
    [param: Required, MaxLength(120)]
    string FullName,

    [param: Required, MaxLength(120)]
    string Specialty
);
