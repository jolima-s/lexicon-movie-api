using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LexiconMovieApi.Data.Entities
{
    public class MovieDetails : EntityBase
    {
        [Required]
        [MaxLength(1000)]
        public string Synopsis { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Language { get; set; } = string.Empty;

        [Precision(18, 2)]
        public decimal Budget { get; set; }

        [ForeignKey("MovieId")]
        public Movie? Movie { get; set; }
        public int MovieId { get; set; }
    }
}
