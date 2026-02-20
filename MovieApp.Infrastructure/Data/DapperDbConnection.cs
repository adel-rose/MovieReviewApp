using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MovieApp.Application.Interfaces;
using System.Data;

namespace MovieApp.Infrastructure.Data
{
    public class DapperDbConnection : IDapperDbConnection
    {
        public readonly string _connectionString;

        public DapperDbConnection(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
