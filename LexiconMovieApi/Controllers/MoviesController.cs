using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LexiconMovieApi.Data.Entities;
using LexiconMovieApi.Data;
using AutoMapper;
using LexiconMovieApi.DTOs.Movie;
using AutoMapper.QueryableExtensions;

[Route("api/movies")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly MovieDbContext _context;
    private readonly IMapper _mapper;
    public MoviesController(MovieDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/movies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
    {
        var movies = await _context.Movies
            .ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return Ok(movies);
    }

    // GET: api/movies/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);

        if (movie == null)
            return NotFound();

        var dto = _mapper.Map<MovieDto>(movie);

        return Ok(dto);
    }

    // GET: api/movies/5/details
    [HttpGet("{id}/details")]
    public async Task<ActionResult<MovieDetailedDto>> GetMovieWithDetails(int id, bool withActors = false, bool withReviews = false, bool withGenres = false)
    {
        IQueryable<Movie> query = _context.Movies
            .Include(m => m.Details);

        if (withActors) query = query.Include(m => m.Actors);
        if (withReviews) query = query.Include(m => m.Reviews);
        if (withGenres) query = query.Include(m => m.Genres);

        var movie = await query.SingleOrDefaultAsync(m => m.Id == id);

        if (movie == null)
            return NotFound();

        var dto = _mapper.Map<MovieDetailedDto>(movie);

        return Ok(dto);
    }

    // PUT: api/movies/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMovie(int? id, MovieUpdateDto movie)
    {
        if (id != movie.Id)
            return BadRequest();

        var entity = _mapper.Map<Movie>(movie);
        _context.Entry(entity).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MovieExists(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // POST: api/movies
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<MovieDto>> PostMovie(MovieCreateDto movie)
    {
        var entity = _mapper.Map<Movie>(movie);
        _context.Movies.Add(entity);
        await _context.SaveChangesAsync();

        var dto = _mapper.Map<MovieDto>(entity);

        return CreatedAtAction("GetMovie", new { id = entity.Id }, dto);
    }

    // DELETE: api/movies/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int? id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
            return NotFound();

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MovieExists(int? id)
    {
        return _context.Movies.Any(e => e.Id == id);
    }
}
