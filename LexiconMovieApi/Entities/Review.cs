using System.ComponentModel.DataAnnotations;

namespace LexiconMovieApi.Entities
{
    public class Review : EntityBase
    {
        [Required]
        [MaxLength(50)]
        public string Reviewer { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Comment { get; set; } = string.Empty;

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public Movie Movie { get; set; } = null!;
        public int MovieId { get; set; }
    }
}
