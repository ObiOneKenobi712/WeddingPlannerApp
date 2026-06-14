using Microsoft.AspNetCore.Mvc;
using WeddingApp.DTOs;
using WeddingApp.Models;
using WeddingApp.Services;

namespace WeddingApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly IExpensesService _expensesService;

    public ExpensesController(IExpensesService expensesService)
    {
        _expensesService = expensesService;
    }

    /// <summary>
    /// Pobiera liste wszystkich wydatków przypisanych do wskazanego wesela.
    /// </summary>
    /// <param name="id">Identyfikator wesela</param>
    /// <returns>Lista wydatków</returns>
    /// <response code="200">Zwraca liste wydatków</response>
    /// <response code="404">Nie znaleziono wesela o podanym identyfikatorze</response>
    [HttpGet("{id}/expenses")]
    public ActionResult<IEnumerable<ExpenseModel>> GetExpenses(int id)
    {
        return Ok(_expensesService.GetAll(id));
    }

    /// <summary>
    /// Dodaje nowy wydatek do wskazanego wesela.
    /// </summary>
    /// <param name="id">Identyfikator wesela</param>
    /// <param name="dto">Dane nowego wydatku</param>
    /// <returns>Nowo utworzony wydatek</returns>
    /// <response code="201">Wydatek został dodany pomyślnie</response>
    /// <response code="400">Błędne dane lub przekroczenie budżetu wesela</response>
    /// <response code="404">Nie znaleziono wesela o podanym identyfikatorze</response>
    [HttpPost("{id}/expenses")]
    public ActionResult AddExpense(int id, CreateExpenseDto dto)
    {
        var newId = _expensesService.Create(id, dto);
        return CreatedAtAction(nameof(GetExpenses), new { id, expenseId = newId }, dto);
    }

    /// <summary>
    /// Aktualizuje dane istniejącego wydatku przypisanego do wesela.
    /// </summary>
    /// <param name="id">Identyfikator wesela</param>
    /// <param name="expenseId">Identyfikator wydatku</param>
    /// <param name="dto">Nowe dane wydatku</param>
    /// <response code="204">Wydatek został zaktualizowany pomyślnie</response>
    /// <response code="400">Błędne dane lub przekroczenie budżetu wesela</response>
    /// <response code="404">Nie znaleziono wesela lub wydatku</response>
    [HttpPut("{id}/expenses/{expenseId}")]
    public ActionResult UpdateExpense(int id, int expenseId, UpdateExpenseDto dto)
    {
        _expensesService.Update(id, expenseId, dto);
        return NoContent();
    }

    /// <summary>
    /// Usuwa wydatek przypisany do wesela.
    /// </summary>
    /// <param name="id">Identyfikator wesela</param>
    /// <param name="expenseId">Identyfikator wydatku do usunięcia</param>
    /// <response code="204">Wydatek został usunięty pomyślnie</response>
    /// <response code="404">Nie znaleziono wesela lub wydatku</response>
    [HttpDelete("{id}/expenses/{expenseId}")]
    public ActionResult RemoveExpense(int id, int expenseId)
    {
        _expensesService.Delete(id, expenseId);
        return NoContent();
    }
}