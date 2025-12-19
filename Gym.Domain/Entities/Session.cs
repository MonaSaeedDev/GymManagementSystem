using Gym.Domain.Common;
using Gym.Domain.Enums;

namespace Gym.Domain.Entities;

public class Session : BaseEntity 
{
    public string Title { get; private set; } = null!;
    public int Capacity { get; private set; }
    public SessionStatus Status { get; private set; }
    public int TraineerId { get; private set; }
    public Trainer Traineer { get; private set; } = null!;

    private readonly List<Booking> _bookings = new(); 
    public IReadOnlyCollection<Booking> Bookings => _bookings;

    public DateOnly Date { get; private set; }
    public TimeOnly StartTime { get; private set; }

    private Session() { }
    public Session(
        string title,
        int capacity,
        int trainerId,
        DateOnly date,
        TimeOnly startTime)
    {
        Title = title;
        Capacity = capacity;
        TraineerId = trainerId;
        Date = date;
        StartTime = startTime;
    }

    public void MarkAsOpen()
    {
        EnsureStatusChangeAllowed(SessionStatus.Open);
        Status = SessionStatus.Open;
        SetUpdated();
    }

    public void MarkAsFull()
    {
        EnsureStatusChangeAllowed(SessionStatus.Full);
        Status = SessionStatus.Full;
        SetUpdated();
    }

    public void MarkAsCancelled()
    {
        EnsureStatusChangeAllowed(SessionStatus.Cancelled);
        Status = SessionStatus.Cancelled;
        SetUpdated();
    }

    private void EnsureStatusChangeAllowed(SessionStatus newStatus)
    {
        if (Status == newStatus)
            throw new InvalidOperationException($"Session is already {newStatus}.");

        if (Status == SessionStatus.Cancelled)
            throw new InvalidOperationException("Cannot change status of a cancelled session.");
    }
}