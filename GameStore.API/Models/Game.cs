using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Models;

public class Game
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; } =  string.Empty;
    
    [Required]
    [StringLength(50)]
    public string Genre { get; set; } = String.Empty;
    
    [StringLength(50)]
    public string? Description { get; set; } 
    
    public decimal Price { get; set; }
}