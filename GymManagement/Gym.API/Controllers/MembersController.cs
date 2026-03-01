using Gym.Application.DTOs.Members;
using Gym.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gym.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MembersController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<MemberResponse>> Create([FromBody] CreateMemberRequest request, CancellationToken ct)
    {
        var created = await _memberService.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<MemberResponse>> GetById(int id, CancellationToken ct)
    {
        var member = await _memberService.GetByIdAsync(id, ct);
        return Ok(member);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<MemberListItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IReadOnlyList<MemberListItem>>> List(CancellationToken ct)
    {
        var items = await _memberService.ListAsync(ct);
        return Ok(items);
    }
}
