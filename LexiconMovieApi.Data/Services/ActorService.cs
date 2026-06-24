using AutoMapper;
using LexiconMovieApi.Core.DomainContracts;
using LexiconMovieApi.Core.DTOs.Actor;
using LexiconMovieApi.Core.Entities;
using LexiconMovieApi.Data.Contracts;

namespace LexiconMovieApi.Data.Services;

public class ActorService : IActorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ActorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ActorDto>> GetAllActorsAsync()
    {
        var actors = await _unitOfWork.Actors.GetAllAsync();
        
        return _mapper.Map<IEnumerable<ActorDto>>(actors);
    }

    public async Task<ActorWithMoviesDto> GetActorByIdAsync(int id)
    {
        var actor = await _unitOfWork.Actors.GetByIdWithMoviesAsync(id);
        
        if (actor == null)
            throw new KeyNotFoundException($"Actor with ID {id} not found.");
        
        return _mapper.Map<ActorWithMoviesDto>(actor);
    }

    public async Task<ActorDto> CreateActorAsync(ActorCreateDto actor)
    {
        var newActor = _mapper.Map<Actor>(actor);
        await _unitOfWork.Actors.AddAsync(newActor);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<ActorDto>(newActor);
    }

    public async Task UpdateActorAsync(ActorUpdateDto actor)
    {
        var existingActor = await _unitOfWork.Actors.GetByIdAsync(actor.Id);
        if (existingActor == null)
            throw new KeyNotFoundException($"Actor with ID {actor.Id} not found.");
        _mapper.Map(actor, existingActor);
        await _unitOfWork.Actors.UpdateAsync(existingActor);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteActorAsync(int id)
    {
        var existingActor = await _unitOfWork.Actors.GetByIdAsync(id);
        if (existingActor == null)
            throw new KeyNotFoundException($"Actor with ID {id} not found.");
        await _unitOfWork.Actors.DeleteAsync(id);
        await _unitOfWork.CompleteAsync();
    }

    public async Task AddActorToMovieAsync(int movieId, int actorId)
    {
        var movie = await _unitOfWork.Movies.GetByIdAsync(movieId);
        if (movie == null)
            throw new KeyNotFoundException($"Movie with ID {movieId} not found.");

        var actor = await _unitOfWork.Actors.GetByIdAsync(actorId);
        if (actor == null)
            throw new KeyNotFoundException($"Actor with ID {actorId} not found.");

        movie.Actors.Add(actor);
        await _unitOfWork.CompleteAsync();
    }
}