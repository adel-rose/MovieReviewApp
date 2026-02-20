using MovieApp.Domain.DTOs;
using MovieApp.Domain.Models;

namespace MovieApp.Application.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieReadDTO>> GetMovies(CancellationToken cancellationToken = default);
        Task<MovieReadDTO> GetMovieAsync(Guid movieId, string userPackage, CancellationToken cancellationToken = default);
        Task<IEnumerable<MovieReadDTO>> GetMovieAsync(string filter, CancellationToken cancellationToken = default);
        Task<bool> CreateMovieAsync(MovieCreateDTO movie);
    }
}
