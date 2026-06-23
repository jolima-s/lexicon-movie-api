namespace LexiconMovieApi.Core.Entities;

public class Actor : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public int BirthYear { get; set; }
    public ICollection<Movie> Movies { get; set; } = new List<Movie>();
}