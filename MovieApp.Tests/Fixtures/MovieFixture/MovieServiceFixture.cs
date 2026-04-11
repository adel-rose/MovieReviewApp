using MovieApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using MovieApp.Application.Interfaces;
using MovieApp.Application.Services;

namespace MovieApp.Tests.Fixtures.MovieFixture
{
    /// <summary>
    /// This is a factory that can provide one or a list of Movie objects.
    /// </summary>
    public class MovieServiceFixture
    {
        public MovieService Sut { get; private set; }
        private Mock<IMovieRespository> EfMovieRepoMocked{ get; set; }
        private Mock<IMovieRepositoryContrib> ContribRepoMocked { get; set; }
        private Mock<IGenreRepository> EfGenreRepoMocked { get; set; }
        private Mock<IDiscountable> PlantinumServiceMocked { get; set; }
        private Mock<IDiscountable> GoldServiceMocked { get; set; }
        private List<Mock<IDiscountable>> DiscountServicesMocked { get; set; }
        
        private NullLogger<string> LoggerMocked;

        public MovieServiceFixture()
        {
            // Mocked dependencies
            EfMovieRepoMocked = new Mock<IMovieRespository>();
            ContribRepoMocked = new Mock<IMovieRepositoryContrib>();
            EfGenreRepoMocked = new Mock<IGenreRepository>();
            PlantinumServiceMocked = new Mock<IDiscountable>();
            GoldServiceMocked = new Mock<IDiscountable>();
            DiscountServicesMocked = new List<Mock<IDiscountable>>();
            LoggerMocked = new NullLogger<string>();
            
            RegisterMocks();
            
            Sut = new MovieService(EfMovieRepoMocked.Object,
                ContribRepoMocked.Object, EfGenreRepoMocked.Object,
                DiscountServicesMocked.Select(x => x.Object), LoggerMocked);
        }

        public void RegisterMocks()
        {
            EfMovieRepoMocked
                .Setup(x => x.FindAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(DataSet());

            EfMovieRepoMocked
                .Setup(x => x.FindByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Guid id, CancellationToken token) =>
                    DataSet().FirstOrDefault(item => item.Id == id));

            PlantinumServiceMocked
                .SetupGet(x => x.Name)
                .Returns("Plantinum");

            PlantinumServiceMocked
                .Setup(x => x.GetDiscountedPrice(It.IsAny<Movie>()))
                .Returns((Movie movie) => GetDiscountedMoviePrice(movie, 0.05m));

            GoldServiceMocked
                .SetupGet(x => x.Name)
                .Returns("Gold");

            GoldServiceMocked
                .Setup(x => x.GetDiscountedPrice(It.IsAny<Movie>()))
                .Returns((Movie movie) => GetDiscountedMoviePrice(movie, 0.1m));
            
            DiscountServicesMocked.AddRange(new List<Mock<IDiscountable>>(){PlantinumServiceMocked, GoldServiceMocked});
        }
        
        private List<Movie> DataSet()
        {
            return new List<Movie>()
            {
                new Movie
                {
                    Id = new Guid("0a2802bf-4377-4191-be3a-08de979e6388"),
                    Title = "Outlaw King",
                    YearOfRelease = 2018,
                    Price = 15m,
                    Genres = new List<Genre>()
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "Braveheart",
                    YearOfRelease = 1995,
                    Price = 7.99m,
                    Genres = new List<Genre>()
                }
            };
        }

        private Decimal GetDiscountedMoviePrice(Movie movie, decimal discount)
        {
            return movie.Price - (movie.Price * discount);
        }
        
    }
}
