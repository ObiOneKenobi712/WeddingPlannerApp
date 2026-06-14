using Microsoft.AspNetCore.Mvc;
using WeddingApp.DTOs;
using WeddingApp.Models;
using WeddingApp.Services;

namespace WeddingApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuestsController : ControllerBase
{
    private readonly IGuestsService _guestsService;

    public GuestsController(IGuestsService guestsService)
    {
        _guestsService = guestsService;
    }

    [HttpGet("{id}/guests")]
    public ActionResult<IEnumerable<GuestModel>> GetGuests(int id)
    {
        return Ok(_guestsService.GetAll(id));
    }

    [HttpGet("{id}/guests/{guestId}")]
    public ActionResult<GuestModel> GetGuestById(int id, int guestId)
    {
        return Ok(_guestsService.GetById(id, guestId));
    }

    [HttpPost("{id}/guests")]
    public ActionResult AddGuest(int id, CreateGuestDto dto)
    {
        var newId = _guestsService.Create(id, dto);
        return CreatedAtAction(nameof(GetGuestById), new { id, guestId = newId }, dto);
    }

    [HttpPut("{id}/guests/{guestId}")]
    public ActionResult UpdateGuest(int id, int guestId, UpdateGuestDto dto)
    {
        _guestsService.Update(id, guestId, dto);
        return NoContent();
    }

    [HttpDelete("{id}/guests/{guestId}")]
    public ActionResult RemoveGuest(int id, int guestId)
    {
        _guestsService.Delete(id, guestId);
        return NoContent();
    }
}