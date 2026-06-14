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

    [HttpGet]
    public ActionResult<IEnumerable<WeddingModel>> GetAll()
    {
        return Ok(_weddingsService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<WeddingModel> GetById(int id)
    {
        return Ok(_weddingsService.GetById(id));
    }

    [HttpPost]
    public ActionResult Create(CreateWeddingDto dto)
    {
        var newId = _weddingsService.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = newId }, dto);
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, UpdateWeddingDto dto)
    {
        _weddingsService.Update(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _weddingsService.Delete(id);
        return NoContent();
    }
}