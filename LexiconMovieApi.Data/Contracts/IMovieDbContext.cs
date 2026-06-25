using LexiconMovieApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LexiconMovieApi.Data.Contracts;

public interface IMovieDbContext
{
    DbSet<Movie> Movies { get; set; }
    DbSet<Genre> Genres { get; set; }
    DbSet<MovieDetails> MoviesDetails { get; set; }
    DbSet<Review> Reviews { get; set; }
    DbSet<Actor> Actors { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}