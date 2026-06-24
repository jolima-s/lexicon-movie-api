using LexiconMovieApi.Core.DomainContracts;
using LexiconMovieApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LexiconMovieApi.Data.Repositories;

public class ActorRepository : IActorRepository
{
    private readonly MovieDbContext _context;

    public ActorRepository(MovieDbContext dbContext) =>_context = dbContext;

    public async Task AddAsync(Actor entity)
    {
        _context.Actors.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var actor = await _context.Actors.FindAsync(id);
        if (actor == null)
            throw new KeyNotFoundException($"Actor with ID {id} not found.");

        _context.Actors.Remove(actor);
        await _context.SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(int id)
    {
        return _context.Actors.AnyAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Actor>> GetAllAsync()
    {
        return await _context.Actors.ToListAsync();
    }

    public async Task<IEnumerable<Actor>> GetAllWithMoviesAsync()
    {
        return await _context.Actors
            .Include(a => a.Movies)
            .ToListAsync();
    }

    public async Task<Actor?> GetByIdAsync(int id)
    {
        return await _context.Actors.SingleOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Actor?> GetByIdWithMoviesAsync(int id)
    {
        return await _context.Actors
            .Include(a => a.Movies)
            .SingleOrDefaultAsync(a => a.Id == id);
    }

    public async Task UpdateAsync(Actor entity)
    {
        _context.Actors.Update(entity);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ExistsAsync(entity.Id))
                throw new KeyNotFoundException($"Actor with ID {entity.Id} not found.");
            else
                throw;
        }
    }
}