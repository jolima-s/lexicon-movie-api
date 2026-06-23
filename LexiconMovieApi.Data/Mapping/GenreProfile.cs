using AutoMapper;
using LexiconMovieApi.Core.DTOs.Genre;
using LexiconMovieApi.Core.Entities;

namespace LexiconMovieApi.Data.Mapping;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GenreDto>();
        CreateMap<Genre, GenreWithMoviesDto>();
    }
}