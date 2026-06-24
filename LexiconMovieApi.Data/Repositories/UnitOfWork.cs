using LexiconMovieApi.Core.DomainContracts;
using LexiconMovieApi.Core.Entities;

namespace LexiconMovieApi.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MovieDbContext _context;

    public UnitOfWork(
        MovieDbContext context,
        IMovieRepository movies,
        IRepository<Genre> genres,
        IActorRepository actors,
        IRepository<Review> reviews)
    {
        _context = context;
        Movies = movies;
        Genres = genres;
        Actors = actors;
        Reviews = reviews;
    }

    public IMovieRepository Movies { get; private set; }
    public IRepository<Genre> Genres { get; private set; }
    public IActorRepository Actors { get; private set; }
    public IRepository<Review> Reviews { get; private set; }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}