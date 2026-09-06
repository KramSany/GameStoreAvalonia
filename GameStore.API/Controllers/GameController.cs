using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStore.API.Models;
using GameStore.API.Data;
using GameStore.API.DTOs;

namespace GameStore.API.Controllers;

[Route("/api/v1/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly GameStoreContext _context;
    
    public GameController(GameStoreContext context)
    {
        _context = context;
    }
    
    // get: api/Game
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetGames()
    {
        var query = _context.Games.AsNoTracking();

        var games = await query
            .Select(x => new GameDto
            {
                Id = x.Id,
                Name = x.Name,
                Genre = x.Genre,
                Price = x.Price
            }).ToListAsync();
        return Ok(games);
    }
    
    // get: api/Games/Id
    [HttpGet("{Id}")]
    public async Task<ActionResult<GameDetailsDto>> GetGame([FromRoute] int Id)
    {
        var game = await _context.Games
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == Id);
        if (game == null)
        {
            return NotFound(new {message = "Game not found."});
        }

        var gamedetaiksDto = new GameDetailsDto()
        {
            Id = game.Id,
            Name = game.Name,
            Genre = game.Genre,
            Description = game.Description,
            Price = game.Price
        };
        
        return Ok(gamedetaiksDto);
    }
    
    // post: api/Game/
    [HttpPost]
    public async Task<ActionResult<GameDetailsDto>> PostGame(CreateUpdateGameDto dto)
    {
        var game = new Game
        {
            Name = dto.Name,
            Description = dto.Description,
            Genre = dto.Genre,
            Price = dto.Price
        };
        _context.Games.Add(game);
        await _context.SaveChangesAsync();

        var createdGameDto = new GameDetailsDto
        {
            Id = game.Id,
            Name = game.Name,
            Genre = game.Genre,
            Description = game.Description,
            Price = game.Price
        };
        
        return CreatedAtAction(nameof(GetGame), new { game.Id }, createdGameDto);
    }
    
    // put: api/Games/id
    [HttpPut("{Id}")]
    public async Task<IActionResult> PutGame([FromRoute] int Id, CreateUpdateGameDto dto)
    {
        var game = await _context.Games.FindAsync(Id);

        if (game == null)
        {
            return NotFound(new { message =  $"Game with ID {Id} not found." });
        }
        
        game.Name = dto.Name;
        game.Description = dto.Description;
        game.Genre = dto.Genre;
        game.Price = dto.Price;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Games.Any(x => x.Id == Id)) return NotFound();
            throw;
        }
        
        return NoContent();
    }
    
    // delete: api/Games/id
    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteGame([FromRoute] int Id)
    {
        var game = await _context.Games.FindAsync(Id);
        if (game == null)
        {
            return NotFound(new { message = $"Game with id - {Id} not found foe delete" });
        }
        
        _context.Games.Remove(game);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}