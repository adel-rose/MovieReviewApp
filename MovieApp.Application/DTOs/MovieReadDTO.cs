namespace MovieApp.Domain.DTOs
{
    public class MovieReadDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public IEnumerable<GenreReadDTO>? Genres { get; set; }
        public string ImageUrl { get; set; }
    }
}
