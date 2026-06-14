using Microsoft.AspNetCore.Mvc;
using WeddingApp.DTOs;
using WeddingApp.Models;
using WeddingApp.Services;

namespace WeddingApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeddingsController : ControllerBase
{
    private readonly IWeddingsService _weddingsService;

    public WeddingsController(IWeddingsService weddingsService)
    {
        _weddingsService = weddingsService;
    }

    /// <summary>
    /// Pobiera stronnicowaną listę wesel.
    /// </summary>
    /// <param name="pageNumber">Numer strony (domyślnie 1)</param>
    /// <param name="pageSize">Liczba elementów na stronie (domyślnie 5)</param>
    /// <returns>Lista wesel dla podanej strony</returns>
    [HttpGet]
    public ActionResult<IEnumerable<WeddingModel>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
    {
        return Ok(_weddingsService.GetAll(pageNumber, pageSize));
    }

    /// <summary>
    /// Pobiera wesele po identyfikatorze.
    /// </summary>
    /// <param name="id">Identyfikator wesela (liczba całkowita)</param>
    /// <returns>Szczegóły wesela</returns>
    [HttpGet("{id}")]
    public ActionResult<WeddingModel> GetById(int id)
    {
        return Ok(_weddingsService.GetById(id));
    }

    /// <summary>
    /// Tworzy nowe wesele.
    /// </summary>
    /// <returns>Utworzone wesele (status 201)</returns>
    /// <response code="201">Wesele zostało utworzone</response>
    /// <response code="400">Niepoprawne dane lub złamanie reguły biznesowej</response>
    [HttpPost]
    public ActionResult Create(CreateWeddingDto dto)
    {
        var newId = _weddingsService.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = newId }, dto);
    }

    /// <summary>
    /// Aktualizuje date, miejsce i status aktywności wesela.
    /// </summary>
    /// <param name="id">Identyfikator wesela (liczba całkowita)</param>
    /// <returns>Brak treści (204), gdy aktualizacja się powiedzie</returns>
    /// <response code="204">Wesele zaktualizowane</response>
    /// <response code="404">Nie znaleziono wesela</response>
    [HttpPut("{id}")]
    public ActionResult Update(int id, UpdateWeddingDto dto)
    {
        _weddingsService.Update(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Usuwa wesele logicznie (Soft Delete).
    /// </summary>
    /// <param name="id">Identyfikator wesela (liczba całkowita)</param>
    /// <returns>Brak tresci (204), gdy usuniecie logiczne sie powiedzie</returns>
    /// <response code="204">Wesele usuniete logicznie</response>
    /// <response code="400">Wesele aktywne - nie mozna usunac</response>
    /// <response code="404">Nie znaleziono wesela</response>
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _weddingsService.Delete(id);
        return NoContent();
    }
}