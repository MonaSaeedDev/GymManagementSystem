using Gym.Application.DTOs.Bookings;
using Gym.Application.DTOs.Sessions;
using Gym.Application.Exceptions;
using Gym.Application.Interfaces.Services;
using Gym.Application.Interfaces.UnitOfWork;
using Gym.Domain.Entities;

namespace Gym.Application.Services;

public sealed class BookingService : IBookingService
{
    private readonly IUnitOfWork _uow;

    public BookingService(IUnitOfWork uow) => _uow = uow;

    public async Task<BookingResponse> BookAsync(CreateBookingRequest request, CancellationToken ct = default)
    {
        var member = await _uow.Members.GetByIdAsync(request.MemberId, ct);
        if (member is null)
            throw new NotFoundException($"Member with id {request.MemberId} was not found.");

        var session = await _uow.Sessions.GetByIdAsync(request.SessionId, ct);
        if (session is null)
            throw new NotFoundException($"Session with id {request.SessionId} was not found.");

        var sessionBookings = await _uow.Bookings.FindAsync(b => b.SessionId == request.SessionId, ct);

        if (sessionBookings.Count >= session.Capacity)
            throw new BusinessRuleException("Session is full.");

        if (sessionBookings.Any(b => b.MemberId == request.MemberId))
            throw new BusinessRuleException("Member already booked this session.");

        var booking = new Booking(request.MemberId, request.SessionId);
        await _uow.Bookings.AddAsync(booking, ct);
        await _uow.SaveChangesAsync(ct);

        return MapToResponse(booking, session, member);

    }
    public async Task CancelAsync(int bookingId, CancellationToken ct = default)
    {
        var booking = await _uow.Bookings.GetByIdAsync(bookingId, ct);
        if (booking is null)
            throw new NotFoundException($"Booking with id {bookingId} was not found.");

        _uow.Bookings.Remove(booking);
        await _uow.SaveChangesAsync(ct);
    }
    public async Task<IReadOnlyList<BookingListItem>> ListAsync(CancellationToken ct = default)
    {
        var bookings = await _uow.Bookings.GetAllAsync(ct);

        var members = await _uow.Members.GetAllAsync(ct);
        var sessions = await _uow.Sessions.GetAllAsync(ct);

        var memberNames = members.ToDictionary(m => m.Id, m => m.FullName);
        var sessionTitles = sessions.ToDictionary(s => s.Id, s => s.Title);

        return bookings
            .Select(b => new BookingListItem
            (
                Id: b.Id,
                MemberId: b.MemberId,
                MemberName: memberNames.TryGetValue(b.MemberId, out var mn) ? mn : string.Empty,
                SessionId: b.SessionId,
                SessionTitle: sessionTitles.TryGetValue(b.SessionId, out var st) ? st : string.Empty,
                BookingDate: b.BookingDate
            ))
            .ToList();
    }
    private static BookingResponse MapToResponse(Booking booking, Session session, Member member)
        => new(
            Id: booking.Id,
            MemberId: booking.MemberId,
            MemberName: member.FullName,
            SessionId: booking.SessionId,
            SessionTitle: session.Title,
            BookingDate: booking.BookingDate,
            CreatedAt: booking.CreatedAt,
            UpdatedAt: booking.UpdatedAt
        );

}
