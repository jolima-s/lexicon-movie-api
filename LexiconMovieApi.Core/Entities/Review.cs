namespace LexiconMovieApi.Core.Entities;

public class Review : EntityBase
{
    public string Reviewer { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public int Rating { get; set; }
    public Movie? Movie { get; set; }
    public int MovieId { get; set; }
}