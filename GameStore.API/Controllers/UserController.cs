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
    
    // post: api/User/
    [HttpPost]
    public async Task<ActionResult<UserDto>> PostGame(UserDto dto)
    {
        var user = new User
        {
            Username = dto.Username,
            Password = dto.Password,
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var createdUserDto = new UserDto()
        {
            Id = user.Id,
            Username = user.Username,
            Password = user.Password
        };
        
        return CreatedAtAction(nameof(GetUser), new { user.Id }, createdUserDto);
    }
    
    // put: api/User/id
    [HttpPut("{Id}")]
    public async Task<IActionResult> PutUser([FromRoute] int Id, UserDto dto)
    {
        var user = await _context.Users.FindAsync(Id);

        if (user == null)
        {
            return NotFound(new { message =  $"User with ID {Id} not found." });
        }
        
        user.Username = dto.Username;
        user.Password = dto.Password;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Users.Any(x => x.Id == Id)) return NotFound();
            throw;
        }
        
        return NoContent();
    }
    
    // delete: api/User/id
    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int Id)
    {
        var user = await _context.Users.FindAsync(Id);
        if (user == null)
        {
            return NotFound(new { message = $"User with id - {Id} not found foe delete" });
        }
        
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}