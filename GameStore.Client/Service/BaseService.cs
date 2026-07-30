using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GameStore.Client.Service;

public abstract class BaseService
{
    protected readonly HttpClient HttpClient;
    
    protected BaseService(HttpClient httpClient)
    {
        HttpClient = httpClient;
        HttpClient.BaseAddress = new Uri("https://localhost:7183/api/v1/");
    }

    protected async Task<T?> GetAsync<T>(string url)
    {
        try
        {
            var response = await HttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            return default;
        }
        catch (Exception)
        {
            return default;
        }
    }
}