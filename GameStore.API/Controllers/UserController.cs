using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStore.API.Models;
using GameStore.API.Data;
using GameStore.API.DTOs;
namespace GameStore.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly GameStoreContext _context;
    
    public UserController(GameStoreContext context)
    {
        _context = context;
    }
    
    // get: api/User
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var query = _context.Users.AsNoTracking();

        var users = await query
            .Select(x => new UserDto
            {
                Id = x.Id,
                Username = x.Username,
                Password = x.Password,
            }).ToListAsync();
        return Ok(users);
    }
    
    // api/User/id
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser([FromRoute] int id)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        
        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }

        var userDto = new UserDto()
        {
            Id = user.Id,
            Username = user.Username,
            Password = user.Password,
        };
        
        return Ok(user);
    }
}