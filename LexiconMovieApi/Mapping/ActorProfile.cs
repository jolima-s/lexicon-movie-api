using AutoMapper;
using LexiconMovieApi.Data.Entities;
using LexiconMovieApi.DTOs.Actor;

namespace LexiconMovieApi.Mapping
{
    public class ActorProfile : Profile
    {
        public ActorProfile() 
        { 
            CreateMap<Actor, ActorDto>();
            CreateMap<Actor, ActorWithMoviesDto>();
        }
    }
}
