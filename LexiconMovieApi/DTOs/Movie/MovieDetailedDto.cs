using LexiconMovieApi.Data.Entities;
using LexiconMovieApi.DTOs.Actor;
using LexiconMovieApi.DTOs.Genre;
using LexiconMovieApi.DTOs.Review;
using System.ComponentModel.DataAnnotations;

namespace LexiconMovieApi.DTOs.Movie
{
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
}
