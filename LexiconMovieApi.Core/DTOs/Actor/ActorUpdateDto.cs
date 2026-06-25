using LexiconMovieApi.Core.DTOs.Movie;
using System.ComponentModel.DataAnnotations;

namespace LexiconMovieApi.Core.DTOs.Actor;

public class ActorUpdateDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(1900, 2100)]
    public DateTime BirthDate { get; set; }

    public ICollection<MovieDto> Movies { get; set; } = new List<MovieDto>();
}