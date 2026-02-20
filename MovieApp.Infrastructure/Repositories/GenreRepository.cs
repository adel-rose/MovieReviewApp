using Dapper;
using MovieApp.Application.Interfaces;
using MovieApp.Domain.DTOs;
using System.Data;

namespace MovieApp.Infrastructure.Repositories
{
    public class GenreRepository
    {
        private readonly IDapperDbConnection _dapperDbConnection;

        public GenreRepository(IDapperDbConnection dapperDbConnection)
        {
            _dapperDbConnection = dapperDbConnection;
        }
        public async Task<int> CreateAsync(GenreCreateDTO genre)
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.ExecuteAsync("INSERT INTO Genre(Genre) VALUES(@Name)", new { Name = genre.Genre});
            }
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GenreReadDTO> FindSingleByIdAsync(Guid id)
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<GenreReadDTO>("SELECT * FROM Genre WHERE Id = @Id", new { Id = id });
            } 
        }

        public async Task<IEnumerable<GenreReadDTO>> FindAllAsync()
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<GenreReadDTO>("SELECT Id, Genre FROM Genre");
            }
        }

        public async Task<GenreReadDTO> FindSingleByNameAsync(string name)
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<GenreReadDTO>("SELECT * FROM Genre WHERE Genre = @Name", new { Name = name });
            }
        }

        public async Task<IEnumerable<GenreReadDTO>> FindMultipleByIdAsync(IEnumerable<Guid> Ids)
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<GenreReadDTO>("SELECT * FROM Genre WHERE Id IN @Ids", new { Ids = Ids });
            }
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
