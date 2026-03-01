using Gym.Domain.Common;
using Gym.Domain.Enums;

namespace Gym.Domain.Entities;

public class Session : BaseEntity
{
    public string Title { get; private set; } = null!;
    public DateOnly Date { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public int Capacity { get; private set; }
    public SessionStatus Status { get; private set; }

    public int TrainerId { get; private set; }
    public Trainer Trainer { get; private set; } = null!;

    private readonly List<Booking> _bookings = new();
    public IReadOnlyCollection<Booking> Bookings => _bookings;

    private Session() { }

    public Session(string title, DateOnly date, TimeOnly startTime, int capacity, int trainerId)
        => (Title, Date, StartTime, Capacity, TrainerId, Status)
         = (title, date, startTime, capacity, trainerId, SessionStatus.Open);

    public void MarkAsFull()
    {
        Status = SessionStatus.Full;
        SetUpdated();
    }
}
