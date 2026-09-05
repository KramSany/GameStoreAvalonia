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
}