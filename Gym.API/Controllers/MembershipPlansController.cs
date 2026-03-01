using Gym.Application.DTOs.MembershipPlans;
using Gym.Application.Interfaces.Services;
using Gym.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class MembershipPlansController : ControllerBase
{
    private readonly IMembershipPlanService _membershipPlanService;

    public MembershipPlansController(IMembershipPlanService membershipPlanService)
    {
        _membershipPlanService = membershipPlanService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(MembershipPlanResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<MembershipPlanResponse>> Create(CreateMembershipPlanRequest request, CancellationToken ct = default)
    {
        var created = await _membershipPlanService.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(MembershipPlanResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<MembershipPlanResponse>> GetById(int id, CancellationToken ct = default)
    {
        var membershipPlan = await _membershipPlanService.GetByIdAsync(id, ct);
        return Ok(membershipPlan);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<MembershipPlanListItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<MembershipPlanListItem>>> List(CancellationToken ct = default)
    {
        var membershipPlans = await _membershipPlanService.ListAsync(ct);
        return Ok(membershipPlans);
    }
}
