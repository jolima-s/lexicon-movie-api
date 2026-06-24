namespace LexiconMovieApi.Data.Contracts;

public interface IServiceManager
{
    IMovieService MovieService { get; }
    IActorService ActorService { get; }
    IGenreService GenreService { get; }
    IReviewService ReviewService { get; }
}