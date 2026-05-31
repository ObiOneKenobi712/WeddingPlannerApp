using Microsoft.AspNetCore.Mvc;
using WeddingApp.Models;
using WeddingApp.DTOs;
using WeddingApp.Data;

namespace WeddingApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuestsController : ControllerBase
{
    [HttpGet("{id}/guests")]
    public IActionResult GetGuests(int id)
    {
        var wedding = WeddingData.Weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        return Ok(wedding.Guests);
    }
    
    [HttpPost("{id}/guests")]
    public IActionResult AddGuest(int id, CreateGuestDto dto)
    {
        var wedding = WeddingData.Weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        var guest = new GuestModel
        {
            Id = wedding.Guests.Count + 1,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            IsConfirmed = dto.IsConfirmed
        };

        wedding.Guests.Add(guest);

        return Ok(guest);
    }
    
    [HttpPut("{id}/guests/{guestId}")]
    public IActionResult UpdateGuest(int id, int guestId, CreateGuestDto dto)
    {
        var wedding = WeddingData.Weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        var guest = wedding.Guests.FirstOrDefault(g => g.Id == guestId);

        if (guest == null)
        {
            return NotFound();
        }

        guest.FirstName = dto.FirstName;
        guest.LastName = dto.LastName;
        guest.IsConfirmed = dto.IsConfirmed;

        return Ok(guest);
    }
    
    [HttpDelete("{id}/guests/{guestId}")]
    public IActionResult RemoveGuest(int id, int guestId)
    {
        var wedding = WeddingData.Weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        var guest = wedding.Guests.FirstOrDefault(g => g.Id == guestId);

        if (guest == null)
        {
            return NotFound();
        }

        wedding.Guests.Remove(guest);

        return NoContent();
    }

    [HttpGet("{id}/guests/{guestId}")]
    public IActionResult GetGuestById(int id, int guestId)
    {
        var wedding = WeddingData.Weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        var guest = wedding.Guests.FirstOrDefault(g => g.Id == guestId);

        if (guest == null)
        {
            return NotFound();
        }

        return Ok(guest);
    }
}