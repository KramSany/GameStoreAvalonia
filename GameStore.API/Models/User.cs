using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Models;

public class User
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Username { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Password { get; set; }
}