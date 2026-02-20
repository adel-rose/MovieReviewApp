using MovieApp.Domain.Models;
using System.Linq.Expressions;

namespace MovieApp.Application.Interfaces
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> FindAllAsync();
        Task<IEnumerable<Genre>> FindByConditionAsync(Expression<Func<Genre, bool>> expression);
        Task<Genre> FindByIdAsync(Guid id);
        Task<bool> CreateAsync(Genre genre);
        Task<bool> UpdateAsync(Genre updatedGenre);
        Task<bool> DeleteAsync(Genre updatedGenre);
    }
}
