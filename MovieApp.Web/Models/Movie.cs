namespace MovieApp.Web.Models;

public class Movie
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int YearOfRelease { get; set; }
    public decimal Price { get; set; }
    public decimal? DiscountedPrice { get; set; }
    public IEnumerable<Genre>? Genres { get; set; }
}