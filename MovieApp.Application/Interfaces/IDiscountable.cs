using MovieApp.Domain.Models;

namespace MovieApp.Application.Interfaces
{
    public interface IDiscountable
    {
        public string Name { get; }
        decimal GetDiscountedPrice(Movie movie);
    }
}
