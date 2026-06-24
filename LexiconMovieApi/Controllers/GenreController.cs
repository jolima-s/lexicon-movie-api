using LexiconMovieApi.Core.DTOs.Genre;
using LexiconMovieApi.Data.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LexiconMovieApi.Client.Controllers;

[Route("api/genres")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public GenreController(IServiceManager serviceManager) => _serviceManager = serviceManager;

    // GET: api/genres
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenreDto>>> GetGenres()
    {
        var genres = await _serviceManager.GenreService.GetAllGenresAsync();

        return Ok(genres);
    }

    // GET: api/genres/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GenreWithMoviesDto>> GetGenre(int id)
    {
        var genre = await _serviceManager.GenreService.GetGenreWithMoviesByIdAsync(id);

        if (genre == null)
            return NotFound();

        return Ok(genre);
    }

    // PUT: api/genres/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[HttpPut("{id}")]
    //public async Task<IActionResult> PutGenre(int? id, GenreUpdateDto genre)
    //{
    //    if (id != genre.Id)
    //        return BadRequest();

    //    try
    //    {
    //        await _serviceManager.GenreService.UpdateGenreAsync(genre);
    //    }
    //    catch (KeyNotFoundException)
    //    {
    //        return NotFound();
    //    }
    //    catch
    //    {
    //        return StatusCode(500, "An error occurred while updating the genre.");
    //    }

    //    return NoContent();
    //}

    // POST: api/genres
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[HttpPost]
    //public async Task<ActionResult<GenreDto>> PostGenre(GenreCreateDto genre)
    //{
    //    GenreDto entity;

    //    try
    //    {
    //        entity = await _serviceManager.GenreService.CreateGenreAsync(genre);
    //    }
    //    catch
    //    {
    //        return StatusCode(500, "An error occurred while creating the genre.");
    //    }

    //    return CreatedAtAction("GetGenre", new { id = entity.Id }, entity);
    //}

    // DELETE: api/genres/5
    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteGenre(int? id)
    //{
    //    if (id == null)
    //        return BadRequest();

    //    try
    //    {
    //        await _serviceManager.GenreService.DeleteGenreAsync(id.Value);
    //    }
    //    catch (KeyNotFoundException)
    //    {
    //        return NotFound();
    //    }
    //    catch
    //    {
    //        return StatusCode(500, "An error occurred while deleting the genre.");
    //    }

    //    return NoContent();
    //}
}