using Gym.Application.DTOs.Trainers;
using Gym.Application.Exceptions;
using Gym.Application.Interfaces.Services;
using Gym.Application.Interfaces.UnitOfWork;
using Gym.Domain.Entities;

namespace Gym.Application.Services;

public sealed class TrainerService : ITrainerService
{
    private readonly IUnitOfWork _uow;

    public TrainerService(IUnitOfWork uow) => _uow = uow;

    public async Task<TrainerResponse> CreateAsync(CreateTrainerRequest request, CancellationToken ct = default)
    {
        var trainer = new Trainer(request.FullName, request.Specialty);

        await _uow.Trainers.AddAsync(trainer, ct);
        await _uow.SaveChangesAsync(ct);

        return MapToResponse(trainer);
    }

    public async Task<TrainerResponse> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var trainer = await _uow.Trainers.GetByIdAsync(id, ct);
        if (trainer is null)
            throw new NotFoundException($"Trainer with id {id} was not found.");

        return MapToResponse(trainer);
    }

    public async Task<IReadOnlyList<TrainerListItem>> ListAsync(CancellationToken ct = default)
    {
        var trainers = await _uow.Trainers.ListAsync(ct);

        return trainers
            .Select(t => new TrainerListItem(
                Id: t.Id,
                FullName: t.FullName,
                Specialty: t.Specialty
            ))
            .ToList();
    }

    private static TrainerResponse MapToResponse(Trainer trainer)
        => new(
            Id: trainer.Id,
            FullName: trainer.FullName,
            Specialty: trainer.Specialty,
            CreatedAt: trainer.CreatedAt,
            UpdatedAt: trainer.UpdatedAt
        );
}
