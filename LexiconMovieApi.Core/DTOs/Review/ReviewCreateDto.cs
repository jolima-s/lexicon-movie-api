namespace LexiconMovieApi.Core.DTOs.Review;

public class ReviewCreateDto
{
    public string Reviewer { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public int Rating { get; set; }
}