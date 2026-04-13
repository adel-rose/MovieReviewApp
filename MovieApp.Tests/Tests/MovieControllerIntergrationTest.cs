using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using MovieApp.Domain.DTOs;

namespace MovieApp.Tests.Tests;

public class MovieControllerIntergrationTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    
    public MovieControllerIntergrationTest(WebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Theory]
    [InlineData("0a2802bf-4377-4191-be3a-08de979e6388", "Plantinum")]
    public async Task WhenGetMovie_GivenPlatinumUserPackage_ShouldReturnDiscountedPriceOf5Percent(Guid movieId, string userPackage)
    {
        // Arrange
        var discountRate = 0.05m;
        
        // Act
        var sut = await _httpClient.GetAsync($"/api/movies/{movieId}?userPackage={userPackage}");
        Assert.NotNull(sut);
        
        var response = await sut.Content.ReadFromJsonAsync<MovieReadDTO>();
    
        // Assert
        Assert.Equal(HttpStatusCode.OK, sut.StatusCode);
        Assert.NotNull(response);
        Assert.Equal(response.Price - (response.Price * discountRate), response.DiscountedPrice);
    }

    [Theory]
    [InlineData("king")]
    public async Task WhenGetMovie_GivenFilter_ShouldReturnOnlyMovieMatchingFilter(string title)
    {
        // Arrange
        
        
        // Act
        var sut = await _httpClient.GetAsync($"api/movies/filter?title={title}");
        Assert.NotNull(sut);

        var response = await sut.Content.ReadFromJsonAsync<List<MovieReadDTO>>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, sut.StatusCode);
        Assert.NotNull(response);
        Assert.NotEmpty(response);
        Assert.All(response, item => Assert.Contains(title.ToLower(), item.Title.ToLower()));
    }
    
    
}