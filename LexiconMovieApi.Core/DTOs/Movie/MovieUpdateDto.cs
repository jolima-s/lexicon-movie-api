using LexiconMovieApi.Core.DTOs.Actor;
using LexiconMovieApi.Core.DTOs.Genre;
using LexiconMovieApi.Core.DTOs.Review;
using LexiconMovieApi.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace LexiconMovieApi.Core.DTOs.Movie;

public class MovieUpdateDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Range(1900, 2100)]
    public int ReleaseYear { get; set; }

    [Range(0, double.MaxValue)]
    public double Duration { get; set; }

    public MovieDetails? Details { get; set; }

    public ICollection<GenreDto> Genres { get; set; } = new List<GenreDto>();

    public ICollection<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();

    public ICollection<ActorDto> Actors { get; set; } = new List<ActorDto>();
}