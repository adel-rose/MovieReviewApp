namespace MovieApp.Web.Contracts;

public interface IHttpClientService
{
    Task<T?> GetAsync<T>(string baseUrl, string endpoint);
    Task<T?> PostAsync<T>(string baseUrl, string endpoint, object body);
    Task<T?> PutAsync<T>(string baseUrl, string endpoint, object body);
    Task DeleteAsync(string baseUrl, string endpoint);
}