using LexiconMovieApi.Core.DTOs.Actor;
using LexiconMovieApi.Core.DTOs.Genre;
using LexiconMovieApi.Core.DTOs.Review;

namespace LexiconMovieApi.Core.DTOs.Movie;

public class MovieDetailedDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public double Duration { get; set; }
    public MovieDetailsDto? Details { get; set; }
    public ICollection<GenreDto> Genres { get; set; } = new List<GenreDto>();
    public ICollection<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
    public ICollection<ActorDto> Actors { get; set; } = new List<ActorDto>();
}