using Microsoft.EntityFrameworkCore;
using MovieApp.Application.Interfaces;
using MovieApp.Domain.Models;
using MovieApp.Infrastructure.Data;
using System.Linq.Expressions;

namespace MovieApp.Infrastructure.Repositories
{
    public class MovieRepositoryEF : IMovieRespository
    {
        private readonly DataContext _dataContext;
        public MovieRepositoryEF(DataContext dataContext)
        {
           _dataContext = dataContext; 
        }

        public async Task<bool> CreateAsync(Movie movie)
        {
            await _dataContext.Movies.AddAsync(movie);

            var created = await _dataContext.SaveChangesAsync();

            return created > 0;
        }

        public async Task<bool> DeleteAsync(Movie movie)
        {
            _dataContext.Movies.Remove(movie);

            var deleted = await _dataContext.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<IEnumerable<Movie>> FindAllAsync(CancellationToken cancellationToken)
        {
            return await _dataContext.Movies.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Movie>> FindByConditionAsync(Expression<Func<Movie, bool>> expression, CancellationToken cancellationToken)
        {
            return await _dataContext.Movies.Where(expression).ToListAsync(cancellationToken);
        }

        public async Task<Movie> FindByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dataContext.Movies.FindAsync(id, cancellationToken);
        }

        public async Task<bool> UpdateAsync(Movie updatedMovie)
        {
            _dataContext.Movies.Update(updatedMovie);

            var updated = await _dataContext.SaveChangesAsync();

            return updated > 0;
        }
    }
}
