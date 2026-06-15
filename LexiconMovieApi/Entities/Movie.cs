using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LexiconMovieApi.Entities
{
    public class Movie : EntityBase
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Range(1900, 2100)]
        public int ReleaseYear { get; set; }

        [Range(0, double.MaxValue)]
        public double Duration { get; set; }

        public MovieDetails? Details { get; set; }

        public ICollection<Genre> Genres { get; set; } = new List<Genre>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
