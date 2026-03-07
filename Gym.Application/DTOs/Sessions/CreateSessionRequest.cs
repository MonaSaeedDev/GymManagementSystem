using System.ComponentModel.DataAnnotations;

namespace Gym.Application.DTOs.Sessions;

public sealed record CreateSessionRequest(
    [param: Required, MaxLength(120)]
    string Title,

    DateOnly Date,
    TimeOnly StartTime,

    [param: Range(1, 200)]
    int Capacity,

    [param: Range(1, int.MaxValue)]
    int TrainerId
) : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Date < DateOnly.FromDateTime(DateTime.Now))
        {
            yield return new ValidationResult(
                "Date must be today or in the future.",
                [nameof(Date)]);
        }
    }
}