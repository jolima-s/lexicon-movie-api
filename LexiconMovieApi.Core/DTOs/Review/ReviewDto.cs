namespace LexiconMovieApi.Core.DTOs.Review;

public class ReviewDto
{
    public int Id { get; set; }
    public string Reviewer { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public int Rating { get; set; }
}