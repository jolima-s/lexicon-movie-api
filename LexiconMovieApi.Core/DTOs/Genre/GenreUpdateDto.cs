using System.ComponentModel.DataAnnotations;

namespace LexiconMovieApi.Core.DTOs.Genre;

public class GenreUpdateDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
}