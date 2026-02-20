using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieApp.Application.Interfaces;
using MovieApp.Domain.DTOs;

namespace MovieApp.Api.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ILogger<string> _logger;
        public MovieController(IMovieService movieService, ILogger<string> logger)
        {
            _movieService = movieService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies(CancellationToken cancellationToken = default)
        {
            try
            {
                var movies = await _movieService.GetMovies(cancellationToken);

                if(movies.IsNullOrEmpty())
                {
                    throw new NullReferenceException("No movies found.");
                }

                return Ok(movies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all movies.");

                if (ex is NullReferenceException)
                {
                    return NotFound("No movies found.");
                }

                return StatusCode(500, "Error while fetching all movies."); 
            }
        }

        [HttpGet("{movieId}")]
        public async Task<ActionResult<MovieReadDTO>> GetMovie([FromRoute] Guid movieId, [FromQuery] string? userPackage)
        {
            var movie = await _movieService.GetMovieAsync(movieId, userPackage);

            if (movie is null)
                return BadRequest();

            return Ok(movie);
        }

        [HttpGet("/filter/{movieId}")]
        public async Task<ActionResult<MovieReadDTO>> GetMovieFiltered([FromRoute] Guid movieId, [FromQuery] string filter)
        {
            try
            {
                var result = await _movieService.GetMovieAsync(filter);

                if(result.IsNullOrEmpty())
                {
                    var message = $"No movies corresponding to {filter} were found.";
                    throw new NullReferenceException(message);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                if (ex is NullReferenceException)
                {
                    return NotFound();
                }

                return StatusCode(500, "An error occured while filtering movies.");
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMovie(MovieCreateDTO movie)
        {
            var createdMovieId = await _movieService.CreateMovieAsync(movie);

            if(!createdMovieId)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetMovie), new { movieId = createdMovieId }, null);
        }


    }
}
