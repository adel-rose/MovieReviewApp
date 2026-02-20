using Microsoft.EntityFrameworkCore;
using MovieApp.Application.Interfaces;
using MovieApp.Domain.Models;
using MovieApp.Infrastructure.Data;
using System.Linq.Expressions;

namespace MovieApp.Infrastructure.Repositories
{
    public class GenreRepositoryEF : IGenreRepository
    {
        private readonly DataContext _dataContext;
        public GenreRepositoryEF(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<bool> CreateAsync(Genre genre)
        {
            await _dataContext.AddAsync(genre);

            var created = _dataContext.SaveChanges();

            return created > 0;
        }

        public async Task<bool> DeleteAsync(Genre updatedGenre)
        {
            _dataContext.Remove(updatedGenre);

            var deleted = _dataContext.SaveChanges();

            return deleted > 0;
        }

        public async Task<IEnumerable<Genre>>FindAllAsync()
        {
            return await _dataContext.Genres.ToListAsync();
        }

        public async Task<IEnumerable<Genre>> FindByConditionAsync(Expression<Func<Genre, bool>> expression)
        {
            return await _dataContext.Genres.Where(expression).ToListAsync();
        }

        public async Task<Genre> FindByIdAsync(Guid id)
        {
            return await _dataContext.Genres.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Genre updatedGenre)
        {
            _dataContext.Update(updatedGenre);

            var updated = _dataContext.SaveChanges();

            return updated > 0;
        }
    }
}
