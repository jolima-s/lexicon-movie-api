namespace LexiconMovieApi.Core.DomainContracts;

public interface IUnitOfWork
{
    IRepository<Movie> Movies { get; }
    IRepository<Genre> Genres { get; }
    IRepository<Actor> Actors { get; }
    IRepository<Review> Reviews { get; }

    Task CompleteAsync();
}