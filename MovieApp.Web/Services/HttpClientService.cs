using MovieApp.Web.Contracts;

namespace MovieApp.Web.Services;

public class HttpClientService : IHttpClientService
{
    private static HttpClient _sharedClient = new(){};


    public async Task<T?> GetAsync<T>(string baseUrl, string endpoint)
    {
        var response = await _sharedClient.GetAsync($"{baseUrl}/{endpoint}");
        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task<T?> PostAsync<T>(string baseUrl, string endpoint, object body)
    {
        var response = await _sharedClient.PostAsJsonAsync($"{baseUrl}/{endpoint}", body);
        return await response.Content.ReadFromJsonAsync<T>();
    }

    public Task<T?> PutAsync<T>(string baseUrl, string endpoint, object body)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string baseUrl, string endpoint)
    {
        throw new NotImplementedException();
    }
}