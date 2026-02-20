using Dapper;
using Microsoft.IdentityModel.Tokens;
using MovieApp.Application.Interfaces;
using MovieApp.Domain.Models;
using System.Data;

namespace MovieApp.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepositoryDapper
    {
        private readonly IDapperDbConnection _dapperDbConnection;
        public MovieRepository(IDapperDbConnection dapperDbConnection)
        {
            _dapperDbConnection = dapperDbConnection;
        }
        public async Task<IEnumerable<Movie>> FindAllAsync()
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<Movie>("SELECT * FROM Movies");
            }
        }
        public async Task<Movie> FindByIdAsync(Guid id)
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<Movie>("SELECT * FROM Movies WHERE Id = @movieId", new { movieId = id });
            }
        }
        public async Task<Guid> CreateAsync(Movie movie)
        {
            
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {

                // Insert the movie first
                var movieTitle = movie.Title;
                var movieYOR = movie.YearOfRelease;
                var movieId = await db.ExecuteScalarAsync<Guid>("INSERT INTO Movies (Title, YearOfRelease) OUTPUT Inserted.Id VALUES (@title, @YOR)", new { title = movieTitle, YOR = movieYOR });

                return movieId;
            }
            
        }
        public async Task CreateMovieGenreMapping(Guid movieId, List<Guid> genreIds)
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            { 
                if(!genreIds.IsNullOrEmpty())
                {
                    foreach (var id in genreIds)
                    {
                        await db.ExecuteAsync("INSERT INTO MovieGenreMapping (MovieId, GenreId) VALUES (@movieId, @genreId)", new { movieId = movieId, genreId = id });
                    }
                }
            }
        }
        public async Task<IEnumerable<MovieGenreMapping>> GetMovieGenreMapping(Guid movieId)
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<MovieGenreMapping>("SELECT * FROM MovieGenreMapping WHERE MovieId = @movieId", new { movieId = movieId });
            }
        }

        public async Task<IEnumerable<Movie>> GetMovieFilteredByYearGenreCategory(int year, string genre, string category)
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                var sql = @"SELECT m.*, g.*, c.* 
                            FROM Movies m
                            JOIN Genres g ON m.GenreCode = g.Code
                            JOIN Categories c ON g.CategoryId = c.CategoryId
                            WHERE m.ReleaseYear > @year
                              AND g.Name = @genre
                              AND c.Name = @category";

                return await db.QueryAsync<Movie>(sql, new {year = year, genre = genre, category = category });
            }
        }

        public Task<bool> UpdateAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}
