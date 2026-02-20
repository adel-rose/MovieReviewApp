using MovieApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Application.Interfaces
{
    public interface IMovieRepositoryContrib
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
