using MovieApp.Domain.Models;
using System.Linq.Expressions;

namespace MovieApp.Application.Interfaces
{
    public interface IMovieRespository
    {
        Task<IEnumerable<Movie>> FindAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Movie>> FindByConditionAsync(Expression<Func<Movie, bool>> expression, CancellationToken cancellationToken);
        Task<Movie> FindByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> CreateAsync(Movie movie);
        Task<bool> UpdateAsync(Movie updatedMovie);
        Task<bool> DeleteAsync(Movie updatedMovie);
    }
}
