using MovieApp.Application.Interfaces;
using MovieApp.Application.Services;
using MovieApp.Domain.Models;

namespace MovieApp.Tests.Tests
{
    public class GetMovieTests
    {

        [Theory]
        [InlineData(100, 95)]
        [InlineData(50, 47.5)]
        [InlineData(0, 0)]
        public void  WhenUserIsPlantinum_Apply5PercentDiscount_GetDiscountedPrice(decimal originalPrice, decimal expectedPrice)
        {
            //Arrange
            var plantinumPakcage = new PlantinumPackage();
            var movie = CreateMovie(originalPrice);

            //Act (exectute, call...)
            var discountedPrice = plantinumPakcage.GetDiscountedPrice(movie);

            //Assert (compare, assume...)
            Assert.Equal(expectedPrice, discountedPrice);
        }

        [Theory]
        [InlineData(100, 90)]
        [InlineData(50, 45)]
        [InlineData(0, 0)]
        public void WhenUserIsGold_Apply10PercentDiscount_GetDiscountedPrice(decimal originalPrice, decimal expectedPrice)
        {

            //Arrange
            var plantinumPakcage = new GoldPackage();
            var movie = CreateMovie(originalPrice);

            //Act (exectute, call...)
            var discountedPrice = plantinumPakcage.GetDiscountedPrice(movie);

            //Assert (compare, assume...)
            Assert.Equal(expectedPrice, discountedPrice);
        }

        private static Movie CreateMovie(decimal originalPrice)
        {
            return new Movie()
            {
                Id = Guid.NewGuid(),
                Title = "Test Movie",
                YearOfRelease = 2025,
                Price = originalPrice
            };
        }

    }
}
