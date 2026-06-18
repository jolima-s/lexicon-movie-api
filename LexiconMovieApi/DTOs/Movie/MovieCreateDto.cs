using System.ComponentModel.DataAnnotations;

namespace LexiconMovieApi.DTOs.Movie
{
    public class MovieCreateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Range(1900, 2100)]
        public int ReleaseYear { get; set; }

        [Range(0, double.MaxValue)]
        public double Duration { get; set; }
    }
}
