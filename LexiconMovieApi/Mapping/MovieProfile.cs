using AutoMapper;
using LexiconMovieApi.Data.Entities;
using LexiconMovieApi.DTOs.Movie;

namespace LexiconMovieApi.Mapping
{
    public class MovieProfile : Profile
    {
        public MovieProfile() 
        { 
            CreateMap<Movie, MovieDto>();
            CreateMap<Movie, MovieDetailedDto>();
            CreateMap<MovieCreateDto, Movie>();
            CreateMap<MovieUpdateDto, Movie>();
            CreateMap<MovieDetails, MovieDetailsDto>();
        }
    }
}
