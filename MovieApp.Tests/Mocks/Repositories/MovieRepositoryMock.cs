using Moq;
using MovieApp.Application.Interfaces;
using MovieApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Tests.Mocks.Repositories
{
    internal static class MovieRepositoryMock
    {
        public static Mock<IMovieRespository> movieRepositoryMocked(IEnumerable<Movie> testData)
        {
            var mockedRepository = new Mock<IMovieRespository>();
            mockedRepository
                .Setup(mock => mock.FindAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(testData);

            return mockedRepository;
        }

        public static Mock<IMovieRepositoryContrib> movieDapperContribRepositoryMocked()
        {
            return new Mock<IMovieRepositoryContrib>();
        }
    }
}
