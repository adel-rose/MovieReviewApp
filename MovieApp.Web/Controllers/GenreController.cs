using Microsoft.AspNetCore.Mvc;
using MovieApp.Web.Contracts;
using MovieApp.Web.Models;

namespace MovieApp.Web.Controllers;

[Route("genre")]
public class GenreController : Controller
{
    private IHttpClientService _httpClientService;
    
    public GenreController(IHttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }

    [HttpGet("list")]
    public async Task<IActionResult> Get()
    {
        var baseUrl = "http://localhost:5180/api";
        var getEndpoint = "genre";

        var genres = await _httpClientService.GetAsync<List<Genre>>(baseUrl, getEndpoint);
        
        return View("ListAllGenres", genres);
    }
    
    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        return View("CreateGenre", new Genre());
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create(Genre genre)
    {
        var baseUrl = "http://localhost:5180/api";
        var createEndpoint = "create";
        var getEndpoint = "genre";


        await _httpClientService.PostAsync<ObjectResult>(baseUrl, createEndpoint, genre);

        var genres = await _httpClientService.GetAsync<List<Genre>>(baseUrl, getEndpoint);
        
        return View("ListAllGenres", genres);
    }
    
}