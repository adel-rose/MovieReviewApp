using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using MovieApp.Application.Interfaces;
using MovieApp.Domain.Models;
using System.Data;

namespace MovieApp.Infrastructure.Repositories
{
    public class MovieRepositoryContrib : IMovieRepositoryContrib
    {
        private readonly IDapperDbConnection _dapperDbConnection;
        private readonly ILogger<string> _logger;
        public MovieRepositoryContrib(IDapperDbConnection dapperDbConnection, ILogger<string> logger)
        {
            _dapperDbConnection = dapperDbConnection;
            _logger = logger;
        }

        public async Task<Guid> CreateAsync(Movie movie)
        {
            try
            {
                if (movie is null)
                {
                    throw new ArgumentNullException(nameof(movie));
                }

                movie.Id = Guid.NewGuid();

                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    await db.InsertAsync(movie);
                }

                return movie.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while trying to insert the new row.");
                throw;
            }
        }

        public Task CreateMovieGenreMapping(Guid movieId, List<Guid> genreIds)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Movie> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MovieGenreMapping>> GetMovieGenreMapping(Guid movieId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
