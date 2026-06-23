using AutoMapper;
using AutoMapper.QueryableExtensions;
using LexiconMovieApi.Core.DTOs.Genre;
using LexiconMovieApi.Core.Entities;
using LexiconMovieApi.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LexiconMovieApi.Data.Repositories;

public class GenreRepository : IRepository<Genre>
{
    private readonly MovieDbContext _context;
    private readonly IMapper _mapper;

    public GenreRepository(MovieDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public async Task AddAsync(Genre entity)
    {
        _context.Genres.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null)
            throw new KeyNotFoundException($"Genre with ID {id} not found.");

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(int id)
    {
        return _context.Genres.AnyAsync(g => g.Id == id);
    }

    public async Task<IEnumerable<Genre>> GetAllAsync()
    {
        return await _context.Genres
            .Include(g => g.Movies)
            .ToListAsync();
    }

    public async Task<Genre?> GetByIdAsync(int id)
    {
        return await _context.Genres
            .Include(g => g.Movies)
            .SingleOrDefaultAsync(g => g.Id == id);
    }

    public async Task UpdateAsync(Genre entity)
    {
        _context.Genres.Update(entity);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ExistsAsync(entity.Id))
                throw new KeyNotFoundException($"Genre with ID {entity.Id} not found.");
            else
                throw;
        }
    }

    public async Task<IEnumerable<GenreDto>> GetAllDtoAsync()
    {
        var genres = await _context.Genres
        .ProjectTo<GenreDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

        return genres;
    }
}