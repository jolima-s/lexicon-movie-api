using LexiconMovieApi.Core.DTOs.Review;

namespace LexiconMovieApi.Data.Contracts;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetAllReviewsAsync();
    Task<ReviewDto> GetReviewByIdAsync(int id);
    Task<ReviewDto> CreateReviewAsync(ReviewCreateDto review);
    Task UpdateReviewAsync(ReviewUpdateDto review);
    Task DeleteReviewAsync(int id);
}