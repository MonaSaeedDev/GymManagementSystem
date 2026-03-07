using System.ComponentModel.DataAnnotations;

namespace Gym.Application.DTOs.Bookings;

public sealed record CreateBookingRequest(
    [param: Range(1, int.MaxValue), Required] int MemberId,
    [param: Range(1, int.MaxValue)] int SessionId
);
