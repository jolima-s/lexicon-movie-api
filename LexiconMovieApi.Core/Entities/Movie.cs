namespace LexiconMovieApi.Core.Entities;

public class Movie : EntityBase
{
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public double Duration { get; set; }
    public MovieDetails? Details { get; set; }
    public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Actor> Actors { get; set; } = new List<Actor>();
}