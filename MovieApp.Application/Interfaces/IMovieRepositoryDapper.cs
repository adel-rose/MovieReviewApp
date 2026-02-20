using MovieApp.Domain.Models;

namespace MovieApp.Application.Interfaces
{
    public interface IMovieRepositoryDapper
    {
        Task<IEnumerable<Movie>> FindAllAsync();
        Task<Movie> FindByIdAsync(Guid id);
        Task<Guid> CreateAsync(Movie movie);
        Task CreateMovieGenreMapping(Guid movieId, List<Guid> genreIds);
        Task<IEnumerable<MovieGenreMapping>> GetMovieGenreMapping(Guid movieId);
        Task<bool> UpdateAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
