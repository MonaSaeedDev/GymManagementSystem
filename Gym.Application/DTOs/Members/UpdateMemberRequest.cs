using System.ComponentModel.DataAnnotations;

namespace Gym.Application.DTOs.Members;

public sealed record UpdateMemberRequest(
    [param: Required, MaxLength(120)]
    string FullName,


    [param: Required, EmailAddress, MaxLength(200)]
    string Email,

    [param: Required,
    RegularExpression(@"^(01[0125][0-9]{8}|\+201[0125][0-9]{8})$",
    ErrorMessage = "Phone must be a valid Egyptian mobile number (010xxxxxxxx or +201xxxxxxxxx)")]
    string Phone,
    DateOnly MembershipStartDate,
    DateOnly MembershipEndDate,

    [param: Required, Range(1, int.MaxValue)]
    int MembershipPlanId
    ) : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (MembershipEndDate < MembershipStartDate)
        {
            yield return new ValidationResult(
                "MembershipEndDate must be greater than or equal to MembershipStartDate.",
                [nameof(MembershipStartDate), nameof(MembershipEndDate)]);
        }
    }
}
