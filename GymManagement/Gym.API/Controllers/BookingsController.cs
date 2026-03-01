using Gym.Application.DTOs.Bookings;
using Gym.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gym.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<BookingResponse>> Book([FromBody] CreateBookingRequest request, CancellationToken ct)
    {
        var created = await _bookingService.BookAsync(request, ct);
        // No GetById in the service yet, so we return 201 with body only
        return StatusCode(StatusCodes.Status201Created, created);
    }

    [HttpDelete("{bookingId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Cancel(int bookingId, CancellationToken ct)
    {
        await _bookingService.CancelAsync(bookingId, ct);
        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<BookingListItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<BookingListItem>>> List(CancellationToken ct)
    {
        var items = await _bookingService.ListAsync(ct);
        return Ok(items);
    }
}
