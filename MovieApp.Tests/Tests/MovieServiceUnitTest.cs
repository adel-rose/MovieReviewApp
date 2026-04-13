using System.Linq.Expressions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using MovieApp.Application.Interfaces;
using MovieApp.Application.Services;
using MovieApp.Domain.DTOs;
using MovieApp.Domain.Models;
using MovieApp.Tests.Fixtures.MovieFixture;
using Serilog;

namespace MovieApp.Tests.Tests
{
    public class MovieServiceUnitTest : IClassFixture<MovieServiceFixture>
    {
        private readonly MovieServiceFixture _movieServiceFixture;

        public MovieServiceUnitTest(MovieServiceFixture movieServiceFixture)
        {
            _movieServiceFixture = movieServiceFixture;
        }
        
        [Theory]
        [InlineData("0a2802bf-4377-4191-be3a-08de979e6388", "Plantinum")]
        public async Task WhenPremiumUserRequestMovieDetail_GivenPlantinumPackage_ShouldGetMovieDetailsWithDiscountedPrice(Guid movieId, string userPackage)
        {
            // Arrange
            var sut = _movieServiceFixture.Sut;
            Assert.NotNull(sut);
            
            // Act
            
            var result = await sut.GetMovieAsync(movieId, userPackage, CancellationToken.None);
            
            // Assert
            Assert.NotNull(result.DiscountedPrice);
            Assert.Equal(result.Price - (result.Price * 0.05m), result.DiscountedPrice);
        }
        
        [Theory]
        [InlineData("0a2802bf-4377-4191-be3a-08de979e6388", "Gold")]
        public async Task WhenPremiumUserRequestMovieDetail_GivenGold_ShouldGetMovieDetailsWithDiscountedPrice(Guid movieId, string userPackage)
        {
            // Arrange
            var sut = _movieServiceFixture.Sut;
            Assert.NotNull(sut);
            
            // Act
            var result = await sut.GetMovieAsync(movieId, userPackage, CancellationToken.None);
            
            // Assert
            Assert.NotNull(result.DiscountedPrice);
            Assert.Equal(result.Price - (result.Price * 0.1m), result.DiscountedPrice);
        }
        
       
    }
}
