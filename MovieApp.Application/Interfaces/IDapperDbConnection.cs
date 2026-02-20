using System.Data;

namespace MovieApp.Application.Interfaces
{
    public interface IDapperDbConnection
    {
        IDbConnection CreateConnection();
    }
}
