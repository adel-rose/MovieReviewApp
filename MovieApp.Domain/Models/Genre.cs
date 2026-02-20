using System.ComponentModel.DataAnnotations;

namespace MovieApp.Domain.Models
{
    public class Genre
    {
        [Key]
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; } = string.Empty;
        public virtual ICollection<Movie> Movies { get; set; }

        public Genre() { }
    }
}
