using Gym.Application.DTOs.Sessions;
using Gym.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gym.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class SessionsController : ControllerBase
{
    private readonly ISessionService _sessionService;

    public SessionsController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(SessionResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<SessionResponse>> Create(CreateSessionRequest request, CancellationToken ct = default)
    {
        var created = await _sessionService.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(SessionResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<SessionResponse>> GetById(int id, CancellationToken ct = default)
    {
        var session = await _sessionService.GetByIdAsync(id, ct);
        return Ok(session);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<SessionListItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<SessionListItem>>> List(CancellationToken ct = default)
    {
        var sessions = await _sessionService.ListAsync(ct);
        return Ok(sessions);
    }
}