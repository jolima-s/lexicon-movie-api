using LexiconMovieApi.Core.DTOs.Movie;
using LexiconMovieApi.Core.DTOs.Review;
using LexiconMovieApi.Data.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LexiconMovieApi.Client.Controllers;

[Route("api/reviews")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public ReviewsController(IServiceManager serviceManager) => _serviceManager = serviceManager;

    // GET: api/reviews
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews()
    {
        var reviews = await _serviceManager.ReviewService.GetAllReviewsAsync();

        return Ok(reviews);
    }

    // GET: api/reviews/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewDto>> GetReview(int id)
    {
        var review = await _serviceManager.ReviewService.GetReviewByIdAsync(id);

        if (review == null)
            return NotFound();

        return Ok(review);
    }

    // PUT: api/reviews/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutReview(int? id, ReviewUpdateDto review)
    {
        if (id != review.Id)
            return BadRequest();

        try
        {
            await _serviceManager.ReviewService.UpdateReviewAsync(review);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch
        {
            return StatusCode(500, "An error occurred while updating the review.");
        }

        return NoContent();
    }

    // POST: api/reviews
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ReviewDto>> PostReview(ReviewCreateDto review)
    {
        ReviewDto entity;

        try
        {
            entity = await _serviceManager.ReviewService.CreateReviewAsync(review);
        }
        catch
        {
            return StatusCode(500, "An error occurred while creating the review.");
        }

        return CreatedAtAction("GetReview", entity);
    }

    // DELETE: api/reviews/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int? id)
    {
        if (id == null)
            return BadRequest();

        try
        {
            await _serviceManager.ReviewService.DeleteReviewAsync(id.Value);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch
        {
            return StatusCode(500, "An error occurred while deleting the review.");
        }

        return NoContent();
    }
}