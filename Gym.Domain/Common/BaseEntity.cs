namespace Gym.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; protected set; }

    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }

    protected void SetUpdated() => UpdatedAt = DateTime.UtcNow;
}