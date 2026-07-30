namespace GameStore.API.DTOs;

public class GameDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } =  string.Empty;
    
    public string Genre { get; set; } = String.Empty;
    
    public string? Description { get; set; } 
    
    public decimal Price { get; set; }
}