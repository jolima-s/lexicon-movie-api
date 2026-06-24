using LexiconMovieApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LexiconMovieApi.Core.DomainContracts;

public interface IMovieRepository : IRepository<Movie>
{
    Task<Movie?> GetByIdWithDetailsAsync(int id, bool withActors = false, bool withReviews = false, bool withGenres = false);
}