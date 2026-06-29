using LexiconMovieApi.Core.DomainContracts;
using LexiconMovieApi.Core.Entities;
using LexiconMovieApi.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace LexiconMovieApi.Data.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly IMovieDbContext _context;

    public MovieRepository(IMovieDbContext dbContext) => _context = dbContext;

    public async Task AddAsync(Movie entity)
    {
        _context.Movies.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
            throw new KeyNotFoundException($"Movie with ID {id} not found.");

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(int id)
    {
        return _context.Movies.AnyAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        return await _context.Movies.ToListAsync();
    }

    public async Task<IEnumerable<Movie>> GetMoviesAsync(int? releaseYear = null, double? duration = null)
    {
        IQueryable<Movie> query = _context.Movies;

        if (releaseYear.HasValue)
            query = query.Where(m => m.ReleaseYear == releaseYear.Value);
        if (duration.HasValue)
            query = query.Where(m => m.Duration == duration.Value);

        return await query.ToListAsync();
    }

    public async Task<Movie?> GetByIdAsync(int id)
    {
        return await _context.Movies
            .SingleOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Movie?> GetByIdWithDetailsAsync(int id, bool withActors = false, bool withReviews = false, bool withGenres = false)
    {
        IQueryable<Movie> query = _context.Movies
            .Include(m => m.Details);

        if (withActors) query = query.Include(m => m.Actors);
        if (withReviews) query = query.Include(m => m.Reviews);
        if (withGenres) query = query.Include(m => m.Genres);
        
        return await query.SingleOrDefaultAsync(m => m.Id == id);
    }

    public async Task UpdateAsync(Movie entity)
    {
        _context.Movies.Update(entity);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ExistsAsync(entity.Id))
                throw new KeyNotFoundException($"Movie with ID {entity.Id} not found.");
            else
                throw;
        }
    }

    public async Task UpdateMovieDetailsAsync(MovieDetails movieDetails)
    {
        _context.MoviesDetails.Update(movieDetails);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ExistsAsync(movieDetails.Id))
                throw new KeyNotFoundException($"Movie with ID {movieDetails.Id} not found.");
            else
                throw;
        }
    }
}