using AutoMapper;
using LexiconMovieApi.Core.DTOs.Review;
using LexiconMovieApi.Core.Entities;

namespace LexiconMovieApi.Data.Mapping;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<Review, ReviewDto>();
        CreateMap<ReviewCreateDto, Review>();
        CreateMap<ReviewUpdateDto, Review>();
    }
}