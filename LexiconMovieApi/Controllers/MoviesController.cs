using Asp.Versioning;
using LexiconMovieApi.Core.DTOs.Movie;
using LexiconMovieApi.Core.DTOs.Review;
using LexiconMovieApi.Data.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LexiconMovieApi.Client.Controllers;

[Route("api/movies")]
[Route("api/v{version:apiVersion}/movies")]
[ApiController]
[ApiVersion("1.0")]
[ApiVersion("1.5")]
[ApiVersion("2.0")]
public class MoviesController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public MoviesController(IServiceManager serviceManager) => _serviceManager = serviceManager;

    // GET: api/v1/movies
    // GET: api/v1/movies?releaseYear=2020&duration=120
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies(int? releaseYear = null, double? duration = null)
    {
        var movies = await _serviceManager.MovieService.GetMoviesAsync(releaseYear, duration);

        return Ok(movies);
    }

    // GET: api/v1/movies/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetMovie(int id)
    {
        var movie = await _serviceManager.MovieService.GetMovieByIdAsync(id);

        if (movie == null)
            return NotFound();

        return Ok(movie);
    }

    // GET: api/v1/movies/5/details
    [HttpGet("{id}/details")]
    public async Task<ActionResult<MovieDetailedDto>> GetMovieWithDetails(int id, bool withActors = false, bool withReviews = false, bool withGenres = false)
    {
        var movie = await _serviceManager.MovieService.GetMovieWithDetailsAsync(id, withActors, withReviews, withGenres);

        if (movie == null)
            return NotFound();

        return Ok(movie);
    }

    // PUT: api/v1/movies/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMovie(int? id, MovieUpdateDto movie)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id != movie.Id)
            return BadRequest();

        try
        {
            await _serviceManager.MovieService.UpdateMovieAsync(movie);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch 
        {
            return StatusCode(500, "An error occurred while updating the movie.");
        }

        return NoContent();
    }

    // PATCH: api/v1/movies/5
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchMovie(int? id, MovieUpdateDto movie)
    {
        return StatusCode(501, "Not Implemented: Partially updating movies is not supported in this version of the API.");
    }

    // POST: api/v1/movies
    [HttpPost]
    public async Task<ActionResult<MovieDto>> PostMovie(MovieCreateDto movie)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        MovieDto entity;

        try
        {
            entity = await _serviceManager.MovieService.CreateMovieAsync(movie);
        }
        catch
        {
            return StatusCode(500, "An error occurred while creating the movie.");
        }

        return CreatedAtAction("GetMovie", entity);
    }

    // DELETE: api/v1/movies/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int? id)
    {
        if (id == null)
            return BadRequest();

        try
        {
            await _serviceManager.MovieService.DeleteMovieAsync(id.Value);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch
        {
            return StatusCode(500, "An error occurred while deleting the movie.");
        }

        return NoContent();
    }
}
