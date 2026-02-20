using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using MovieApp.Application.Interfaces;
using MovieApp.Application.Services;
using MovieApp.Domain.DTOs;
using MovieApp.Tests.Fixtures.MovieFixture;
using MovieApp.Tests.Mocks.Repositories;

namespace MovieApp.Tests.Tests
{
    public class MovieServiceTest
    {
        /// <summary>
        /// Movie service should be able to map list of movie objects received from data access to DTOs
        /// </summary>
        [Fact]
        public async Task WhenGetAllMoviesServiceIsCalled_GivenAMockedListOfMovieObjects_ShouldReturnAListOfMovieReadDTO()
        {
            //Arrange
            var testData = MovieFixture.CreateMultiple(5);

            var mockedRepository = MovieRepositoryMock.movieRepositoryMocked(testData);
            var mockedMovieRepositoryContrib = MovieRepositoryMock.movieDapperContribRepositoryMocked();
            var mockedGenreRepository = GenreRepositoryMock.GenreReposotoryMocked();
            var mockedPackages = new List<Mock<IDiscountable>>();
            var mockedLogger = new NullLogger<string>();

            var sut = new MovieService(mockedRepository.Object, mockedMovieRepositoryContrib.Object, mockedGenreRepository.Object, mockedPackages.Select(mocks => mocks.Object), mockedLogger);

            //Act
            var result = await sut.GetMovies();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IEnumerable<MovieReadDTO>>(result);
        }
    }
}
