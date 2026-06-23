using LexiconMovieApi.Data.Contracts;

namespace LexiconMovieApi.Data.Services;

public class ServiceManager : IServiceManager
{
    public ServiceManager(
        IMovieService movieService, 
        IActorService actorService, 
        IGenreService genreService, 
        IReviewService reviewService)
    {
        MovieService = movieService;
        ActorService = actorService;
        GenreService = genreService;
        ReviewService = reviewService;
    }

    public IMovieService MovieService { get; private set; }
    public IActorService ActorService { get; private set; }
    public IGenreService GenreService { get; private set; }
    public IReviewService ReviewService { get; private set; }
}