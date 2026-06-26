using Asp.Versioning;
using LexiconMovieApi.Core.DTOs.Actor;
using LexiconMovieApi.Data.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LexiconMovieApi.Client.Controllers;

[Route("api/actors")]
[Route("api/v{version:apiVersion}/actors")]
[ApiController]
[ApiVersion("1.0")]
public class ActorsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public ActorsController(IServiceManager serviceManager) => _serviceManager = serviceManager;

    // GET: api/v1/actors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActorDto>>> GetActors()
    {
        var actors = await _serviceManager.ActorService.GetAllActorsAsync();

        return Ok(actors);
    }

    // GET: api/v1/actors/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ActorWithMoviesDto>> GetActor(int id)
    {
        var actor = await _serviceManager.ActorService.GetActorByIdAsync(id);

        if (actor == null)
            return NotFound();

        return Ok(actor);
    }

    // PUT: api/v1/actors/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutActor(int? id, ActorUpdateDto actor)
    {
        if (id != actor.Id)
            return BadRequest();

        try
        {
            await _serviceManager.ActorService.UpdateActorAsync(actor);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch
        {
            return StatusCode(500, "An error occurred while updating the actor.");
        }

        return NoContent();
    }

    // POST: api/v1/actors
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ActorDto>> PostActor(ActorCreateDto actor)
    {
        ActorDto entity;

        try
        {
            entity = await _serviceManager.ActorService.CreateActorAsync(actor);
        }
        catch
        {
            return StatusCode(500, "An error occurred while creating the actor.");
        }

        return CreatedAtAction("GetActor", new { id = entity.Id }, entity);
    }

    // DELETE: api/v1/actors/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActor(int? id)
    {
        if (id == null)
            return BadRequest();

        try
        {
            await _serviceManager.ActorService.DeleteActorAsync(id.Value);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch
        {
            return StatusCode(500, "An error occurred while deleting the actor.");
        }

        return NoContent();
    }

    // POST: api/v1/movies/2/actors/4
    [HttpPost("api/v1/movies/{movieId}/actors/{actorId}")]
    public async Task<IActionResult> AddActorToMovie(int movieId, int actorId)
    {
        try
        {
            await _serviceManager.ActorService.AddActorToMovieAsync(movieId, actorId);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch
        {
            return StatusCode(500, "An error occurred while adding the actor to the movie.");
        }

        return NoContent();
    }
}
