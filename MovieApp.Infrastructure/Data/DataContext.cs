using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieApp.Domain.Models;

namespace MovieApp.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :
            base(options)
        {
            
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }

    }
}
