using Microsoft.Extensions.Logging;
using MovieApp.Application.Extensions;
using MovieApp.Application.Interfaces;
using MovieApp.Domain.DTOs;
using MovieApp.Domain.Models;
using System.Threading;

namespace MovieApp.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRespository _movieRepository;
        private readonly IMovieRepositoryContrib _movieRepositoryContrib;
        private readonly IGenreRepository _genreRepository;
        private readonly IEnumerable<IDiscountable> _packages;
        private readonly ILogger<string> _logger;
        public MovieService(IMovieRespository movieRepository, IMovieRepositoryContrib movieRepositoryContrib, IGenreRepository genreRepository, IEnumerable<IDiscountable> packages, ILogger<string> logger)
        {
            _movieRepository = movieRepository;
            _movieRepositoryContrib = movieRepositoryContrib;
            _genreRepository = genreRepository;
            _packages = packages;
            _logger = logger;   
        }

        public async Task<bool> CreateMovieAsync(MovieCreateDTO movie)
        {
            // Need to retrieve all genres assigned to movie first
            var genresForNewMovie = await _genreRepository.FindByConditionAsync(genre => movie.Genre.Contains(genre.Id));

            Movie newMovie = new Movie()
            {
                Title = movie.Title,
                YearOfRelease = movie.YearOfRelease,
                Price = movie.Price,
                DiscountedPrice = movie.DiscountedPrice,
                Genres = genresForNewMovie
            };

            // Persist a movie
            return await _movieRepository.CreateAsync(newMovie);
        }

        public async Task<IEnumerable<MovieReadDTO>> GetMovies(CancellationToken cancellationToken = default)
        {
            var movies = await _movieRepository.FindAllAsync(cancellationToken);

            return movies.Select(movie => new MovieReadDTO()
            {
                Id = movie.Id,
                Title = movie.Title,
                YearOfRelease = movie.YearOfRelease,
                Price= movie.Price,
                Genres = movie.Genres.MapToGenreRead()
            });
        }

        public async Task<MovieReadDTO> GetMovieAsync(Guid movieId, string userPackage, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.FindByIdAsync(movieId, cancellationToken);

            // Using DI, the better polymorphism
            if (userPackage is not null && userPackage != "")
            {
                var package = _packages.Where(package => package.Name.Equals(userPackage)).FirstOrDefault();

                if(package is not null)
                {
                    movie.DiscountedPrice = package.GetDiscountedPrice(movie);
                }
                
                return new MovieReadDTO
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    YearOfRelease = movie.YearOfRelease,
                    Price = movie.Price,
                    DiscountedPrice = movie.DiscountedPrice,
                    Genres = movie.Genres.MapToGenreRead()
                };
            }

          
            return new MovieReadDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                YearOfRelease = movie.YearOfRelease,
                Price = movie.Price,
                DiscountedPrice = movie.DiscountedPrice,
                Genres = movie.Genres.MapToGenreRead()
            };
        }

        public async Task<IEnumerable<MovieReadDTO>> GetMovieAsync(string filter, CancellationToken cancellationToken)
        {
            try
            {
                var filteredMovies = await _movieRepository.FindByConditionAsync(movie => movie.Title.ToLower().Contains(filter.ToLower()), cancellationToken);

                var filteredMoviesDTOs = filteredMovies.MapToMovieRead();

                return filteredMoviesDTOs;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occured while fetching movies by filter.");
                return Enumerable.Empty<MovieReadDTO>();
            }
        }
    }
}
