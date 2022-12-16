namespace WebAPI.Model;

public class Players
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Image { get; set; }
    public string? Country { get; set; }
    public int Rank { get; set; }
    public string? Description { get; set; } 
}
