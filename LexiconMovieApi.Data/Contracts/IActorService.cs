using LexiconMovieApi.Core.DTOs.Actor;

namespace LexiconMovieApi.Data.Contracts;

public interface IActorService
{
    Task<IEnumerable<ActorDto>> GetAllActorsAsync(bool includeMovies = false);
    Task<ActorDto> GetActorByIdAsync(int id, bool includeMovies = false);
    Task UpdateActorAsync(ActorUpdateDto actor);
    Task<ActorDto> CreateActorAsync(ActorCreateDto actor);
    Task DeleteActorAsync(int id);
    Task AddActorToMovieAsync(int movieId, int actorId);
}