using Microsoft.AspNetCore.Mvc;
using WeddingApp.DTOs;
using WeddingApp.Models;
using WeddingApp.Services;

namespace WeddingApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BudgetsController : ControllerBase
{
    private readonly IBudgetsService _budgetsService;

    public BudgetsController(IBudgetsService budgetsService)
    {
        _budgetsService = budgetsService;
    }

    /// <summary>
    /// Tworzy budżet dla wybranego wesela.
    /// </summary>
    /// <param name="id">Identyfikator wesela</param>
    /// <param name="dto">Dane nowego budżetu</param>
    /// <returns>Utworzony budżet</returns>
    /// <response code="201">Budżet został utworzony pomyślnie</response>
    /// <response code="400">Budżet już istnieje lub dane sa niepoprawne</response>
    /// <response code="404">Nie znaleziono wesela</response>
    [HttpPost("{id}/budget")]
    public ActionResult<BudgetModel> CreateBudget(int id, CreateBudgetDto dto)
    {
        var created = _budgetsService.Create(id, dto);
        return CreatedAtAction(nameof(GetBudget), new { id }, created);
    }

    /// <summary>
    /// Pobiera budżet przypisany do wybranego wesela.
    /// </summary>
    /// <param name="id">Identyfikator wesela</param>
    /// <returns>Budżet przypisany do wesela</returns>
    /// <response code="200">Zwraca budżet wesela</response>
    /// <response code="404">Nie znaleziono wesela lub budżetu</response>
    [HttpGet("{id}/budget")]
    public ActionResult<BudgetModel> GetBudget(int id)
    {
        return Ok(_budgetsService.Get(id));
    }

    /// <summary>
    /// Aktualizuje całkowity budżet wybranego wesela.
    /// </summary>
    /// <param name="id">Identyfikator wesela</param>
    /// <param name="dto">Nowe dane budżetu</param>
    /// <returns>Zaktualizowany budżet</returns>
    /// <response code="200">Budżet został zaktualizowany pomyślnie</response>
    /// <response code="400">Nowy budżet jest mniejszy od już wydanej kwoty</response>
    /// <response code="404">Nie znaleziono wesela lub budżetu</response>
    [HttpPut("{id}/budget")]
    public ActionResult<BudgetModel> UpdateBudget(int id, UpdateBudgetDto dto)
    {
        var updated = _budgetsService.Update(id, dto);
        return Ok(updated);
    }

}