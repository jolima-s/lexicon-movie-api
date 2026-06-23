using AutoMapper;
using AutoMapper.QueryableExtensions;
using LexiconMovieApi.Core.DTOs.Movie;
using LexiconMovieApi.Core.Entities;
using LexiconMovieApi.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LexiconMovieApi.Data.Repositories;

public class MovieRepository : IRepository<Movie>
{
    private readonly MovieDbContext _context;
    private readonly IMapper _mapper;

    public MovieRepository(MovieDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

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
        return await _context.Movies
            .Include(m => m.Genres)
            .Include(m => m.Reviews)
            .Include(m => m.Actors)
            .ToListAsync();
    }

    public async Task<Movie?> GetByIdAsync(int id)
    {
        return await _context.Movies
            .SingleOrDefaultAsync(m => m.Id == id);
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

    public async Task<IEnumerable<MovieDto>> GetAllDtoAsync()
    {
        var movies = await _context.Movies
        .ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

        return movies;
    }
}