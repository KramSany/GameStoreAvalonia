using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.Client.DTOs;

namespace GameStore.Client.Service;

public class GameApiService : BaseService
{
    public GameApiService(HttpClient httpClient) : base(httpClient)
    {
    }
    
    public async Task<IEnumerable<GameDto>> GetGamesAsync(string? search = null)
    {
        var url = "Game";

        if (!string.IsNullOrWhiteSpace(search))
        {
            url += $"?search={search}";
        }

        var result = await GetAsync<IEnumerable<GameDto>>(url);
        return result ??  new List<GameDto>();
    }

    public async Task<GameDetailsDto?> GetGameAsync(int id)
    {
        return await GetAsync<GameDetailsDto>($"games/{id}");
    }

}