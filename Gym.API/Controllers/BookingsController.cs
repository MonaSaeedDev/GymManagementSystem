using Gym.Application.DTOs.Bookings;
using Gym.Application.DTOs.Trainers;
using Gym.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<BookingResponse>> Book(CreateBookingRequest request, CancellationToken ct = default)
    {
        var created = await _bookingService.BookAsync(request, ct);
        // No GetById in the service yet, so we return 201 with body only
        return StatusCode(StatusCodes.Status201Created, created);
    }

    [HttpDelete("{bookingId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Cancel(int bookingId, CancellationToken ct = default)
    {
        await _bookingService.CancelAsync(bookingId, ct);
        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<BookingListItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<BookingListItem>>> List(CancellationToken ct = default)
    {
        var bookings = await _bookingService.ListAsync(ct);
        return Ok(bookings);
    }
}
