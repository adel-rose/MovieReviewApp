using MovieApp.Application.Interfaces;
using MovieApp.Domain.Models;

namespace MovieApp.Application.Services
{
    public class PlantinumPackage : IDiscountable
    {
        public string Name  => "Plantinum";
        public decimal GetDiscountedPrice(Movie movie)
        {
            return movie.Price - (movie.Price * 0.05m);
        }
    }
}
