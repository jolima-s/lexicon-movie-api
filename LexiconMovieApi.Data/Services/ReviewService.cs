using AutoMapper;
using LexiconMovieApi.Core.DomainContracts;
using LexiconMovieApi.Core.DTOs.Review;
using LexiconMovieApi.Data.Contracts;

namespace LexiconMovieApi.Data.Services;

public class ReviewService : IReviewService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReviewDto>> GetAllReviewsAsync()
    {
        var reviews = await _unitOfWork.Reviews.GetAllAsync();
        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }

    public async Task<ReviewDto> GetReviewByIdAsync(int id)
    {
        var review = await _unitOfWork.Reviews.GetByIdAsync(id);
        if (review == null)
            throw new KeyNotFoundException($"Review with ID {id} not found.");
        return _mapper.Map<ReviewDto>(review);
    }

    public async Task<ReviewDto> CreateReviewAsync(ReviewCreateDto review)
    {
        var newReview = _mapper.Map<Core.Entities.Review>(review);
        await _unitOfWork.Reviews.AddAsync(newReview);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<ReviewDto>(newReview);
    }

    public async Task UpdateReviewAsync(ReviewUpdateDto review)
    {
        var existingReview = await _unitOfWork.Reviews.GetByIdAsync(review.Id);
        if (existingReview == null)
            throw new KeyNotFoundException($"Review with ID {review.Id} not found.");

        _mapper.Map(review, existingReview);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteReviewAsync(int id)
    {
        var review = await _unitOfWork.Reviews.GetByIdAsync(id);
        if (review == null)
            throw new KeyNotFoundException($"Review with ID {id} not found.");

        await _unitOfWork.Reviews.DeleteAsync(id);
        await _unitOfWork.CompleteAsync();
    }
}
