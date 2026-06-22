using AutoMapper;
using LexiconMovieApi.Data.Entities;
using LexiconMovieApi.DTOs.Genre;

namespace LexiconMovieApi.Mapping
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GenreDto>();
            CreateMap<Genre, GenreWithMoviesDto>();
        }
    }
}
