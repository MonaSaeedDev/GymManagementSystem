using Gym.Application.DTOs.Bookings;

namespace Gym.Application.Interfaces.Services;

public interface IBookingService
{
    Task<BookingResponse> BookAsync(CreateBookingRequest request, CancellationToken ct = default);
    Task CancelAsync(int bookingId, CancellationToken ct = default);
    Task<IReadOnlyList<BookingListItem>> ListAsync(CancellationToken ct = default);
}
