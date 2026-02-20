using MovieApp.Application.Interfaces;
using MovieApp.Domain.DTOs;
using MovieApp.Domain.Models;

namespace MovieApp.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public async Task<bool> CreateGenreAsync(GenreCreateDTO genre)
        {
            var existingGenre = await _genreRepository.FindByConditionAsync(genres => genres.Name == genre.Genre);

            if (existingGenre is not null && existingGenre.Any()) 
            {
                return false;
            }

            Genre newGenre = new Genre()
            {
                Name = genre.Genre
            };
           
            return await _genreRepository.CreateAsync(newGenre); 
        }

        public async Task<IEnumerable<GenreReadDTO>> FindAllGenreAsync()
        {
            var genres = await _genreRepository.FindAllAsync();

            var genreDTOs = genres.Select(genre => new GenreReadDTO() { 
                Id = genre.Id,
                Genre = genre.Name
            });

            return genreDTOs;
        }

        public async Task<GenreReadDTO> FindGenreByIdAsync(Guid guid)
        {
            var genre = await _genreRepository.FindByIdAsync(guid);

            return new GenreReadDTO() { 
                Id = genre.Id,
                Genre = genre.Name
            };
        }
    }
}
