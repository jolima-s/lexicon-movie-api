using LexiconMovieApi.Core.DTOs.Movie;

namespace LexiconMovieApi.Core.DTOs.Genre;

public class GenreWithMoviesDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<MovieDto> Movies { get; set; } = new List<MovieDto>();
}