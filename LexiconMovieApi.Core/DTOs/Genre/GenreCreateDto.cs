using System.ComponentModel.DataAnnotations;

namespace LexiconMovieApi.Core.DTOs.Genre;

public class GenreCreateDto
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
}