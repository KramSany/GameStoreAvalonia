using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.Client.DTOs;

namespace GameStore.Client.Service;

public class UserApiService : BaseService
{
    public  UserApiService(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync(string? search = null)
    {
        var url = "User";

        if (!string.IsNullOrWhiteSpace(search))
        {
            url += $"?search={search}";
        }
        
        var  result = await GetAsync<IEnumerable<UserDto>>(url);
        return result;
    }

    public async Task<UserDto?> GetUserAsync(int id)
    {
        return await GetAsync<UserDto>($"User/{id}");
    }
}