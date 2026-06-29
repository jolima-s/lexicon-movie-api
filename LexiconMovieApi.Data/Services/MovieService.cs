using AutoMapper;
using LexiconMovieApi.Core.DomainContracts;
using LexiconMovieApi.Core.DTOs.Movie;
using LexiconMovieApi.Core.Entities;
using LexiconMovieApi.Data.Contracts;

namespace LexiconMovieApi.Data.Services;

public class MovieService : IMovieService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MovieService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MovieDto>> GetMoviesAsync()
    {
        var movies = await _unitOfWork.Movies.GetAllAsync();
        return _mapper.Map<IEnumerable<MovieDto>>(movies);
    }

    public async Task<IEnumerable<MovieDto>> GetMoviesAsync(int? releaseYear = null, double? duration = null)
    {
        var movies = await _unitOfWork.Movies.GetMoviesAsync(releaseYear, duration);
        return _mapper.Map<IEnumerable<MovieDto>>(movies);
    }

    public async Task<MovieDto?> GetMovieByIdAsync(int id)
    {
        var movie = await _unitOfWork.Movies.GetByIdAsync(id);
        return movie == null
        ? null
        : _mapper.Map<MovieDto>(movie);
    }

    public async Task<MovieDetailedDto> GetMovieWithDetailsAsync(int id, bool withActors = false, bool withReviews = false, bool withGenres = false)
    {
        var movie = await _unitOfWork.Movies.GetByIdWithDetailsAsync(id, withActors, withReviews, withGenres);
        if (movie == null)
            throw new KeyNotFoundException($"Movie with ID {id} not found.");

        return _mapper.Map<MovieDetailedDto>(movie);
    }

    public async Task UpdateMovieAsync(MovieUpdateDto updateDto)
    {
        var movie = await _unitOfWork.Movies.GetByIdAsync(updateDto.Id);
        if (movie == null)
            throw new KeyNotFoundException($"Movie with ID {updateDto.Id} not found.");

        _mapper.Map(updateDto, movie);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<MovieDto> CreateMovieAsync(MovieCreateDto createDto)
    {
        var movie = _mapper.Map<Movie>(createDto);
        await _unitOfWork.Movies.AddAsync(movie);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<MovieDto>(movie);
    }

    public async Task DeleteMovieAsync(int id)
    {
        var movie = await _unitOfWork.Movies.GetByIdAsync(id);
        if (movie == null)
            throw new KeyNotFoundException($"Movie with ID {id} not found.");
        await _unitOfWork.Movies.DeleteAsync(id);
        await _unitOfWork.CompleteAsync();
    }
}