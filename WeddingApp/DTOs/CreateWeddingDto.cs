using System.ComponentModel.DataAnnotations;

namespace WeddingApp.DTOs;

public class CreateWeddingDto
{
    public string BrideName { get; set; } = "";
    public string GroomName { get; set; } = "";
    public DateTime Date { get; set; }
    public string Venue { get; set; } = "";   
}

