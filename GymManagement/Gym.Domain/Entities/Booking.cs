using Gym.Domain.Common;

namespace Gym.Domain.Entities;

public class Booking : BaseEntity
{
    public int MemberId { get; private set; }
    public Member Member { get; private set; } = null!;

    public int SessionId { get; private set; }
    public Session Session { get; private set; } = null!;

    public DateTime BookingDate { get; private set; }

    private Booking() { }

    public Booking(int memberId, int sessionId)
        => (MemberId, SessionId, BookingDate) = (memberId, sessionId, DateTime.UtcNow);
}
