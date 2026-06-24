using LexiconMovieApi.Core.Entities;

namespace LexiconMovieApi.Core.DomainContracts;

public interface IActorRepository : IRepository<Actor>
{
    Task<IEnumerable<Actor>> GetAllWithMoviesAsync();
    Task<Actor?> GetByIdWithMoviesAsync(int id);
}