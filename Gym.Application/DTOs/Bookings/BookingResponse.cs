namespace Gym.Application.DTOs.Bookings;

public sealed record BookingResponse(
    int Id,
    int MemberId,
    string MemberName,
    int SessionId,
    string SessionTitle,
    DateTime BookingDate,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
