using LexiconMovieApi.Core.DomainContracts;
using LexiconMovieApi.Core.Entities;
using LexiconMovieApi.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace LexiconMovieApi.Data.Repositories;

public class ReviewRepository : IRepository<Review>
{
    private readonly IMovieDbContext _context;

    public ReviewRepository(IMovieDbContext dbContext) => _context = dbContext;

    public async Task AddAsync(Review entity)
    {
        _context.Reviews.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
            throw new KeyNotFoundException($"Review with ID {id} not found.");

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(int id)
    {
        return _context.Reviews.AnyAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        return await _context.Reviews
            .Include(r => r.Movie)
            .ToListAsync();
    }

    public async Task<Review?> GetByIdAsync(int id)
    {
        return await _context.Reviews
            .Include(r => r.Movie)
            .SingleOrDefaultAsync(r => r.Id == id);
    }

    public async Task UpdateAsync(Review entity)
    {
        _context.Reviews.Update(entity);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ExistsAsync(entity.Id))
                throw new KeyNotFoundException($"Review with ID {entity.Id} not found.");
            else
                throw;
        }
    }
}