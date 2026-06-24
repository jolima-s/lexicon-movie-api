using LexiconMovieApi.Core.Entities;

namespace LexiconMovieApi.Core.DomainContracts;

public interface IUnitOfWork
{
    IMovieRepository Movies { get; }
    IRepository<Genre> Genres { get; }
    IActorRepository Actors { get; }
    IRepository<Review> Reviews { get; }

    Task CompleteAsync();
}