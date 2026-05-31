using Microsoft.AspNetCore.Mvc;
using WeddingApp.Models;
using WeddingApp.DTOs;
using WeddingApp.Data;

namespace WeddingApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeddingsController : ControllerBase
{
  

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(WeddingData.Weddings);
    }
    
    [HttpPost]
    public IActionResult Create(CreateWeddingDto dto)
    {
        var wedding = new WeddingModel
        {
            Id = WeddingData.Weddings.Count + 1,
            BrideName = dto.BrideName,
            GroomName = dto.GroomName,
            Date = dto.Date,
            Venue = dto.Venue,
            IsActive = true
        };

        WeddingData.Weddings.Add(wedding);

        return Ok(wedding);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var wedding = WeddingData.Weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        return Ok(wedding);
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(int id, CreateWeddingDto dto)
    {
        var wedding = WeddingData.Weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        wedding.BrideName = dto.BrideName;
        wedding.GroomName = dto.GroomName;
        wedding.Date = dto.Date;
        wedding.Venue = dto.Venue;

        return Ok(wedding);
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var wedding = WeddingData.Weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        WeddingData.Weddings.Remove(wedding);

        return NoContent();
    }
    
}