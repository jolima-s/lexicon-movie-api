namespace LexiconMovieApi.Core.DTOs.Movie;

public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public double Duration { get; set; }
}