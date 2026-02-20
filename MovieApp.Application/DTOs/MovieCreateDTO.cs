namespace MovieApp.Domain.DTOs
{
    public class MovieCreateDTO
    {
        public required string Title { get; set; }
        public required int YearOfRelease { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public List<Guid> Genre { get; set; }
    }
}
