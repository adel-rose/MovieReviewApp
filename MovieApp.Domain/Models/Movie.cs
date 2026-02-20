using System.ComponentModel.DataAnnotations;

namespace MovieApp.Domain.Models
{
    public class Movie
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public virtual IEnumerable<Genre> Genres { get; set; }

    }
}
