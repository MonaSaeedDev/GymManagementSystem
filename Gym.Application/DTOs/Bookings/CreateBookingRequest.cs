namespace Gym.Application.DTOs.Bookings;

public sealed record CreateBookingRequest(
    int MemberId,
    int SessionId
);
