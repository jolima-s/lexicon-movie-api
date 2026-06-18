using AutoMapper;
using LexiconMovieApi.Data.Entities;
using LexiconMovieApi.DTOs.Review;

namespace LexiconMovieApi.Mapping
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewDto>();
        }
    }
}
