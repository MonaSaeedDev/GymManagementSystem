using Gym.Application.DTOs.Sessions;
using Gym.Application.DTOs.Trainers;
using Gym.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class TrainersController : ControllerBase
{
    private readonly ITrainerService _trainerService;

    public TrainersController(ITrainerService trainerService)
    {
        _trainerService = trainerService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(TrainerResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<TrainerResponse>> Create(CreateTrainerRequest request, CancellationToken ct = default)
    {
        var created = await _trainerService.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TrainerResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<TrainerResponse>> GetById(int id, CancellationToken ct = default)
    {
        var trainer = await _trainerService.GetByIdAsync(id, ct);
        return Ok(trainer);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TrainerListItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TrainerListItem>>> List(CancellationToken ct = default)
    {
        var trainers = await _trainerService.ListAsync(ct);
        return Ok(trainers);
    }
}