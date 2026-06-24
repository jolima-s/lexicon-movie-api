using AutoMapper;
using LexiconMovieApi.Core.DomainContracts;
using LexiconMovieApi.Core.DTOs.Genre;
using LexiconMovieApi.Data.Contracts;

namespace LexiconMovieApi.Data.Services;

public class GenreService : IGenreService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GenreDto>> GetAllGenresAsync()
    {
        var genres = await _unitOfWork.Genres.GetAllAsync();
        return _mapper.Map<IEnumerable<GenreDto>>(genres);
    }

    public async Task<GenreDto?> GetGenreByIdAsync(int id)
    {
        var genre = await _unitOfWork.Genres.GetByIdAsync(id);
        return _mapper.Map<GenreDto?>(genre);
    }

    public async Task<IEnumerable<GenreWithMoviesDto>> GetAllGenresWithMoviesAsync()
    {
        var genres = await _unitOfWork.Genres.GetAllAsync();
        return _mapper.Map<IEnumerable<GenreWithMoviesDto>>(genres);
    }

    public async Task<GenreWithMoviesDto?> GetGenreWithMoviesByIdAsync(int id)
    {
        var genre = await _unitOfWork.Genres.GetByIdAsync(id);
        return _mapper.Map<GenreWithMoviesDto?>(genre);
    }
}