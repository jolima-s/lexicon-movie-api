namespace LexiconMovieApi.Core.Entities;

public class Genre : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public ICollection<Movie> Movies { get; set; } = new List<Movie>();
}