namespace Gym.Application.DTOs.Bookings;

public sealed record BookingListItem(
    int Id,
    int MemberId,
    string MemberName,
    int SessionId,
    string SessionTitle,
    DateTime BookingDate
);
