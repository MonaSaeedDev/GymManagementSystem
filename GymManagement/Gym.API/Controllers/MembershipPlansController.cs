using Gym.Application.DTOs.MembershipPlans;
using Gym.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gym.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class MembershipPlansController : ControllerBase
{
    private readonly IMembershipPlanService _membershipPlanService;

    public MembershipPlansController(IMembershipPlanService membershipPlanService)
    {
        _membershipPlanService = membershipPlanService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(MembershipPlanResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<MembershipPlanResponse>> Create([FromBody] CreateMembershipPlanRequest request, CancellationToken ct)
    {
        var created = await _membershipPlanService.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(MembershipPlanResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<MembershipPlanResponse>> GetById(int id, CancellationToken ct)
    {
        var plan = await _membershipPlanService.GetByIdAsync(id, ct);
        return Ok(plan);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<MembershipPlanListItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<MembershipPlanListItem>>> List(CancellationToken ct)
    {
        var items = await _membershipPlanService.ListAsync(ct);
        return Ok(items);
    }
}
