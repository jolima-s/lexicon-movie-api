namespace LexiconMovieApi.Core.Entities;

public class MovieDetails : EntityBase
{
    public string Synopsis { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public Movie? Movie { get; set; }
    public int MovieId { get; set; }
}