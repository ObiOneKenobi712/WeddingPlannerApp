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
    /// Pobiera stronicowana liste wesel.
    /// </summary>
    /// <param name="pageNumber">Numer strony (domyslnie 1)</param>
    /// <param name="pageSize">Liczba elementow na stronie (domyslnie 5)</param>
    [HttpGet]
    public ActionResult<IEnumerable<WeddingModel>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
    {
        return Ok(_weddingsService.GetAll(pageNumber, pageSize));
    }

    /// <summary>
    /// Pobiera wesele po identyfikatorze.
    /// </summary>
    [HttpGet("{id}")]
    public ActionResult<WeddingModel> GetById(int id)
    {
        return Ok(_weddingsService.GetById(id));
    }

    /// <summary>
    /// Tworzy nowe wesele.
    /// </summary>
    [HttpPost]
    public ActionResult Create(CreateWeddingDto dto)
    {
        var newId = _weddingsService.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = newId }, dto);
    }

    /// <summary>
    /// Aktualizuje date, miejsce i status aktywnosci wesela.
    /// </summary>
    [HttpPut("{id}")]
    public ActionResult Update(int id, UpdateWeddingDto dto)
    {
        _weddingsService.Update(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Usuwa wesele logicznie (Soft Delete).
    /// </summary>
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _weddingsService.Delete(id);
        return NoContent();
    }
}