using GameStore.API.Models;
using Microsoft.EntityFrameworkCore;
namespace GameStore.API.Data;


public class GameStoreContext : DbContext
{
    public GameStoreContext(DbContextOptions<GameStoreContext> options)
        : base(options)
    {
        
    }
    
    public DbSet<Game> Games { get; set; }
}