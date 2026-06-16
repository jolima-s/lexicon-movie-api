using System.ComponentModel.DataAnnotations;

namespace LexiconMovieApi.Data.Entities
{
    public class Actor : EntityBase
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1900, 2100)]
        public int BirthYear { get; set; }

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
