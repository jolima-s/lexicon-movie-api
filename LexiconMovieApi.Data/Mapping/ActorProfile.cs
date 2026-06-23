using AutoMapper;
using LexiconMovieApi.Core.DTOs.Actor;
using LexiconMovieApi.Core.Entities;

namespace LexiconMovieApi.Data.Mapping;

public class ActorProfile : Profile
{
    public ActorProfile()
    {
        CreateMap<Actor, ActorDto>();
        CreateMap<Actor, ActorWithMoviesDto>();
    }
}