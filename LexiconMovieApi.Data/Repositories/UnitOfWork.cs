using LexiconMovieApi.Core.DomainContracts;

namespace LexiconMovieApi.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MovieDbContext _context;

    public UnitOfWork(
        MovieDbContext context,
        IRepository<Movie> movies,
        IRepository<Genre> genres,
        IRepository<Actor> actors,
        IRepository<Review> reviews)
    {
        _context = context;
        Movies = movies;
        Genres = genres;
        Actors = actors;
        Reviews = reviews;
    }

    public IRepository<Movie> Movies { get; private set; }
    public IRepository<Genre> Genres { get; private set; }
    public IRepository<Actor> Actors { get; private set; }
    public IRepository<Review> Reviews { get; private set; }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}