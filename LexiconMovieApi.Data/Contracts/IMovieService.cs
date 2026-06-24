using LexiconMovieApi.Core.DTOs.Movie;

namespace LexiconMovieApi.Data.Contracts;

public interface IMovieService
{
    Task<IEnumerable<MovieDto>> GetAllMoviesAsync();
    Task<MovieDto> GetMovieByIdAsync(int id);
    Task<MovieDetailedDto> GetMovieWithDetailsAsync(int id, bool withActors = false, bool withReviews = false, bool withGenres = false);
    Task UpdateMovieAsync(MovieUpdateDto updateDto);
    Task<MovieDto> CreateMovieAsync(MovieCreateDto createDto);
    Task DeleteMovieAsync(int id);
}