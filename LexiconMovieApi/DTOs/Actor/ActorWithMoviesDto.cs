using LexiconMovieApi.DTOs.Movie;
using System.ComponentModel.DataAnnotations;

namespace LexiconMovieApi.DTOs.Actor
{
    public class ActorWithMoviesDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BirthYear { get; set; }
        public ICollection<MovieDto> Movies { get; set; } = new List<MovieDto>();
    }
}
