using Gym.Application.DTOs.Trainers;
using Gym.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gym.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TrainersController : ControllerBase
{
    private readonly ITrainerService _trainerService;

    public TrainersController(ITrainerService trainerService)
    {
        _trainerService = trainerService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(TrainerResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<TrainerResponse>> Create([FromBody] CreateTrainerRequest request, CancellationToken ct)
    {
        var created = await _trainerService.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TrainerResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<TrainerResponse>> GetById(int id, CancellationToken ct)
    {
        var trainer = await _trainerService.GetByIdAsync(id, ct);
        return Ok(trainer);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TrainerListItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TrainerListItem>>> List(CancellationToken ct)
    {
        var items = await _trainerService.ListAsync(ct);
        return Ok(items);
    }
}
