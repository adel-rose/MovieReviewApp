using Microsoft.AspNetCore.Mvc;
using MovieApp.Web.Contracts;
using MovieApp.Web.Models;

namespace MovieApp.Web.Controllers;

public class MoviesController : Controller
{
    private IHttpClientService _httpClientService;
    
    public MoviesController(IHttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }
    // GET
    public IActionResult Index()
    {
        return View();
    }
    public async Task<IActionResult> ListAll()
    {
        var baseUrl = "http://localhost:5180/api";
        var endpoint = "movies";
        var movies = await _httpClientService.GetAsync<List<Movie>>(baseUrl, endpoint);
        
        return View("listAllMovies", movies);
    }
    public async Task<IActionResult> Create(Movie movie)
    {
        var baseUrl = "http://localhost:5180/api";
        var endpoint = "create";

        await _httpClientService.PostAsync<ObjectResult>(baseUrl, endpoint, movie);

        var movies = await _httpClientService.GetAsync<List<Movie>>(baseUrl, endpoint);
        
        return View("listAllMovies", movies);
    }
    
}