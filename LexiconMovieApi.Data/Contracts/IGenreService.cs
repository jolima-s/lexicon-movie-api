using LexiconMovieApi.Core.DTOs.Genre;

namespace LexiconMovieApi.Data.Contracts;

public interface IGenreService
{
    Task<IEnumerable<GenreDto>> GetAllGenresAsync();
    Task<GenreDto?> GetGenreByIdAsync(int id);
    Task<IEnumerable<GenreWithMoviesDto>> GetAllGenresWithMoviesAsync();
    Task<GenreWithMoviesDto?> GetGenreWithMoviesByIdAsync(int id);
}