using AutoMapper;
using LexiconMovieApi.Core.DTOs.Movie;
using LexiconMovieApi.Core.Entities;

namespace LexiconMovieApi.Data.Mapping;

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