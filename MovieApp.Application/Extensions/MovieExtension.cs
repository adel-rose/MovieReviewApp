using MovieApp.Domain.DTOs;
using MovieApp.Domain.Models;

namespace MovieApp.Application.Extensions
{
    public static class MovieExtension
    {
        public static IEnumerable<GenreReadDTO> MapToGenreRead(this IEnumerable<Genre> genres)
        {
            IEnumerable<GenreReadDTO> dtos = genres.Select(genre => new GenreReadDTO()
            {
                Id = genre.Id,
                Name = genre.Name
            });

            return dtos;
        }

        public static IEnumerable<MovieReadDTO> MapToMovieRead(this IEnumerable<Movie> movies)
        {
            IEnumerable<MovieReadDTO> dtos = movies.Select(movie => new MovieReadDTO()
            {
                Id = movie.Id,
                Title = movie.Title,
                YearOfRelease = movie.YearOfRelease,
                Price = movie.Price,
                DiscountedPrice = movie.DiscountedPrice,
                Genres = movie.Genres.MapToGenreRead()
            });

            return dtos;
        }
    }
}
