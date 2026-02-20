using Microsoft.AspNetCore.Mvc;
using MovieApp.Application.Interfaces;
using MovieApp.Domain.DTOs;


namespace MovieApp.Api.Controllers
{
    [ApiController]
    [Route("api/genre")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<ActionResult> FindAllGenre()
        {
            var result = await _genreService.FindAllGenreAsync();

            if(result is null || !result.Any())
                return NotFound();

            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateGenre(GenreCreateDTO genre)
        {
            var result = await _genreService.CreateGenreAsync(genre);

            if(result)
                return Created();

            return Conflict();

        }
    }
}
