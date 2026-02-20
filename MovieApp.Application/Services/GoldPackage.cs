using MovieApp.Application.Interfaces;
using MovieApp.Domain.Models;

namespace MovieApp.Application.Services
{
    public class GoldPackage : IDiscountable
    {
        public string Name => "Gold";
        public decimal GetDiscountedPrice(Movie movie)
        {
            return movie.Price - (movie.Price * 0.1m);
        }
    }
}
