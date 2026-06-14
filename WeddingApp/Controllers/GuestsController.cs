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

    /// <summary>
    /// Pobiera liste wszystkich gości dla wesela.
    /// </summary>
    /// <param name="id">Identyfikator wesela</param>
    /// <returns>Lista gości na weselu</returns>
    [HttpGet("{id}/guests")]
    public ActionResult<IEnumerable<GuestModel>> GetGuests(int id)
    {
        return Ok(_guestsService.GetAll(id));
    }

    /// <summary>
    /// Pobiera szczegóły konkretnego gościa weselnego.
    /// </summary>
    /// <param name="id">Identyfikator wesela</param>
    /// <param name="guestId">Identyfikator gościa</param>
    /// <returns>Szczegóły wybranego gościa</returns>
    [HttpGet("{id}/guests/{guestId}")]
    public ActionResult<GuestModel> GetGuestById(int id, int guestId)
    {
        return Ok(_guestsService.GetById(id, guestId));
    }

    /// <summary>
    /// Dodaje nowego gościa na wesele.
    /// </summary>
    /// <param name="id">Identyfikator wesela</param>
    /// <param name="dto">Dane nowego gościa weselnego</param>
    /// <returns>Nowy gość</returns>
    /// <response code="201">Gość został dodany pomyślnie</response>
    /// <response code="400">Błąd walidacji lub duplikat gościa</response>
    [HttpPost("{id}/guests")]
    public ActionResult AddGuest(int id, CreateGuestDto dto)
    {
        var newId = _guestsService.Create(id, dto);
        return CreatedAtAction(nameof(GetGuestById), new { id, guestId = newId }, dto);
    }

    /// <summary>
    /// Aktualizuje dane gościa (imię, nazwisko, potwierdzenie obecności).
    /// </summary>
    /// <param name="id">Identyfikator wesela</param>
    /// <param name="guestId">Identyfikator gościa</param>
    /// <param name="dto">Nowe dane gościa</param>
    /// <response code="204">Gość został zaktualizowany pomyślnie</response>
    /// <response code="404">Wesele lub gość nie znaleziony</response>
    [HttpPut("{id}/guests/{guestId}")]
    public ActionResult UpdateGuest(int id, int guestId, UpdateGuestDto dto)
    {
        _guestsService.Update(id, guestId, dto);
        return NoContent();
    }

    /// <summary>
    /// Usuwa gościa z listy wesela.
    /// </summary>
    /// <param name="id">Identyfikator wesela</param>
    /// <param name="guestId">Identyfikator gościa do usunięcia</param>
    /// <response code="204">Gość został usunięty pomyślnie</response>
    /// <response code="404">Wesele lub gość nie znaleziony</response>
    [HttpDelete("{id}/guests/{guestId}")]
    public ActionResult RemoveGuest(int id, int guestId)
    {
        _guestsService.Delete(id, guestId);
        return NoContent();
    }
}