namespace GameStore.API.DTOs;
using System.ComponentModel.DataAnnotations;

public class CreateUpdateGameDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Genre { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [Range(0, 100000)]
    public decimal Price { get; set; }
}