using Gym.Application.DTOs.Sessions;
using Gym.Application.Exceptions;
using Gym.Application.Interfaces.Services;
using Gym.Application.Interfaces.UnitOfWork;
using Gym.Domain.Entities;

namespace Gym.Application.Services;

public sealed class SessionService : ISessionService
{
    private readonly IUnitOfWork _uow;

    public SessionService(IUnitOfWork uow) => _uow = uow;

    public async Task<SessionResponse> CreateAsync(CreateSessionRequest request, CancellationToken ct = default)
    {
        var trainer = await _uow.Trainers.GetByIdAsync(request.TrainerId, ct);
        if (trainer is null)
            throw new NotFoundException($"Trainer with id {request.TrainerId} was not found.");

        var session = new Session(
            title: request.Title,
            date: request.Date,
            startTime: request.StartTime,
            capacity: request.Capacity,
            trainerId: request.TrainerId
        );

        await _uow.Sessions.AddAsync(session, ct);
        await _uow.SaveChangesAsync(ct);

        return MapToResponse(session, trainerName: trainer.FullName);
    }

    public async Task<SessionResponse> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var session = await _uow.Sessions.GetByIdAsync(id, ct);
        if (session is null)
            throw new NotFoundException($"Session with id {id} was not found.");

        var trainer = await _uow.Trainers.GetByIdAsync(session.TrainerId, ct);
        var trainerName = trainer?.FullName ?? string.Empty;

        return MapToResponse(session, trainerName);
    }

    public async Task<IReadOnlyList<SessionListItem>> ListAsync(CancellationToken ct = default)
    {
        var sessions = await _uow.Sessions.ListAsync(ct);

        return sessions
            .Select(s => new SessionListItem(
                Id: s.Id,
                Title: s.Title,
                Date: s.Date,
                StartTime: s.StartTime,
                Capacity: s.Capacity,
                Status: s.Status.ToString()
            ))
            .ToList();
    }

    private static SessionResponse MapToResponse(Session session, string trainerName)
        => new(
            Id: session.Id,
            Title: session.Title,
            Date: session.Date,
            StartTime: session.StartTime,
            Capacity: session.Capacity,
            TrainerId: session.TrainerId,
            TrainerName: trainerName,
            Status: session.Status.ToString(),
            CreatedAt: session.CreatedAt,
            UpdatedAt: session.UpdatedAt
        );
}
