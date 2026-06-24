using LexiconMovieApi.Core.DTOs.Movie;
using System.ComponentModel.DataAnnotations;

namespace LexiconMovieApi.Core.DTOs.Actor;

public class ActorUpdateDto
{
    [Required]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public ICollection<MovieDto> Movies { get; set; } = new List<MovieDto>();
}