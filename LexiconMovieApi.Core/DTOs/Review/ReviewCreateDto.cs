using System.ComponentModel.DataAnnotations;

namespace LexiconMovieApi.Core.DTOs.Review;

public class ReviewCreateDto
{
    [Required]
    [StringLength(50)]
    public string Reviewer { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]
    public string Comment { get; set; } = string.Empty;

    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }
}