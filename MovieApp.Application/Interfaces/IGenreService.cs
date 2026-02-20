using MovieApp.Domain.DTOs;

namespace MovieApp.Application.Interfaces
{
    public interface IGenreService
    {
        Task<bool> CreateGenreAsync(GenreCreateDTO genre);
        Task<GenreReadDTO> FindGenreByIdAsync(Guid guid);
        Task<IEnumerable<GenreReadDTO>> FindAllGenreAsync();
    }
}
