using System.ComponentModel.DataAnnotations;

namespace LexiconMovieApi.Core.DTOs.Actor;

public class ActorCreateDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(1900, 2100)]
    public DateTime BirthDate { get; set; }
}