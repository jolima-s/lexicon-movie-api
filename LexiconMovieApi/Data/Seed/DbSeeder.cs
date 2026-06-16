using LexiconMovieApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LexiconMovieApi.Data.Seed
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(MovieDbContext context)
        {
            await context.Database.MigrateAsync();

            if (!await context.Movies.AnyAsync())
            {
                var movies = new List<Movie>
                {
                    new Movie
                    {
                        Id = 1,
                        Title = "The Shawshank Redemption",
                        ReleaseYear = 1994,
                        Duration = 142,
                        Details = new MovieDetails
                        {
                            Id = 1,
                            Synopsis = "Two imprisoned men bond over decades while finding hope and redemption behind prison walls.",
                            Language = "English",
                            Budget = 25000000
                        },
                        Genres = new List<Genre>
                        {
                            new Genre { Id = 1, Name = "Drama" }
                        },
                        Reviews = new List<Review>
                        {
                            new Review { Id = 1, Reviewer = "Emily Carter", Rating = 5, Comment = "An inspiring story with unforgettable performances." },
                            new Review { Id = 2, Reviewer = "Michael Thompson", Rating = 5, Comment = "A timeless masterpiece about hope and friendship." }
                        },
                        Actors = new List<Actor>
                        {
                            new Actor { Id = 1, Name = "Tim Robbins", BirthYear = 1958 },
                            new Actor { Id = 2, Name = "Morgan Freeman", BirthYear = 1937 },
                            new Actor { Id = 3, Name = "Bob Gunton", BirthYear = 1945 }
                        }
                    },

                    new Movie
                    {
                        Id = 2,
                        Title = "The Dark Knight",
                        ReleaseYear = 2008,
                        Duration = 152,
                        Details = new MovieDetails
                        {
                            Id = 2,
                            Synopsis = "Batman faces the Joker, a criminal mastermind determined to plunge Gotham City into chaos.",
                            Language = "English",
                            Budget = 185000000
                        },
                        Genres = new List<Genre>
                        {
                            new Genre { Id = 2, Name = "Action" },
                            new Genre { Id = 3, Name = "Crime" },
                            new Genre { Id = 1, Name = "Drama" }
                        },
                        Reviews = new List<Review>
                        {
                            new Review { Id = 3, Reviewer = "Sarah Johnson", Rating = 5, Comment = "A gripping superhero film elevated by an iconic villain." },
                            new Review { Id = 4, Reviewer = "David Wilson", Rating = 5, Comment = "Exceptional action, storytelling, and performances." }
                        },
                        Actors = new List<Actor>
                        {
                            new Actor { Id = 4, Name = "Christian Bale", BirthYear = 1974 },
                            new Actor { Id = 5, Name = "Heath Ledger", BirthYear = 1979 },
                            new Actor { Id = 6, Name = "Aaron Eckhart", BirthYear = 1968 }
                        }
                    },

                    new Movie
                    {
                        Id = 3,
                        Title = "Inception",
                        ReleaseYear = 2010,
                        Duration = 148,
                        Details = new MovieDetails
                        {
                            Id = 3,
                            Synopsis = "A skilled thief enters people's dreams to steal secrets and is offered a chance at redemption.",
                            Language = "English",
                            Budget = 160000000
                        },
                        Genres = new List<Genre>
                        {
                            new Genre { Id = 4, Name = "Science Fiction" },
                            new Genre { Id = 5, Name = "Thriller" },
                            new Genre { Id = 2, Name = "Action" }
                        },
                        Reviews = new List<Review>
                        {
                            new Review { Id = 5, Reviewer = "Jessica Miller", Rating = 5, Comment = "A mind-bending and visually stunning experience." },
                            new Review { Id = 6, Reviewer = "Christopher Brown", Rating = 4, Comment = "Complex, ambitious, and highly rewarding." }
                        },
                        Actors = new List<Actor>
                        {
                            new Actor { Id = 7, Name = "Leonardo DiCaprio", BirthYear = 1974 },
                            new Actor { Id = 8, Name = "Joseph Gordon-Levitt", BirthYear = 1981 },
                            new Actor { Id = 9, Name = "Tom Hardy", BirthYear = 1977 }
                        }
                    },

                    new Movie
                    {
                        Id = 4,
                        Title = "The Lord of the Rings: The Fellowship of the Ring",
                        ReleaseYear = 2001,
                        Duration = 178,
                        Details = new MovieDetails
                        {
                            Id = 4,
                            Synopsis = "A young hobbit begins a perilous journey to destroy a powerful ring.",
                            Language = "English",
                            Budget = 93000000
                        },
                        Genres = new List<Genre>
                        {
                            new Genre { Id = 6, Name = "Fantasy" },
                            new Genre { Id = 7, Name = "Adventure" }
                        },
                        Reviews = new List<Review>
                        {
                            new Review { Id = 7, Reviewer = "Olivia Davis", Rating = 5, Comment = "An epic fantasy adventure that set a new standard." },
                            new Review { Id = 8, Reviewer = "Daniel Martinez", Rating = 5, Comment = "Immersive world-building and unforgettable characters." }
                        },
                        Actors = new List<Actor>
                        {
                            new Actor { Id = 10, Name = "Elijah Wood", BirthYear = 1981 },
                            new Actor { Id = 11, Name = "Ian McKellen", BirthYear = 1939 },
                            new Actor { Id = 12, Name = "Viggo Mortensen", BirthYear = 1958 }
                        }
                    },

                    new Movie
                    {
                        Id = 5,
                        Title = "Pulp Fiction",
                        ReleaseYear = 1994,
                        Duration = 154,
                        Details = new MovieDetails
                        {
                            Id = 5,
                            Synopsis = "Interconnected stories of crime, violence, and redemption unfold in Los Angeles.",
                            Language = "English",
                            Budget = 8000000
                        },
                        Genres = new List<Genre>
                        {
                            new Genre { Id = 3, Name = "Crime" },
                            new Genre { Id = 5, Name = "Thriller" }
                        },
                        Reviews = new List<Review>
                        {
                            new Review { Id = 9, Reviewer = "Sophia Anderson", Rating = 5, Comment = "Sharp dialogue and unforgettable storytelling." },
                            new Review { Id = 10, Reviewer = "James Taylor", Rating = 4, Comment = "Bold, stylish, and endlessly quotable." }
                        },
                        Actors = new List<Actor>
                        {
                            new Actor { Id = 13, Name = "John Travolta", BirthYear = 1954 },
                            new Actor { Id = 14, Name = "Samuel L. Jackson", BirthYear = 1948 },
                            new Actor { Id = 15, Name = "Uma Thurman", BirthYear = 1970 }
                        }
                    },

                    new Movie
                    {
                        Id = 6,
                        Title = "Forrest Gump",
                        ReleaseYear = 1994,
                        Duration = 142,
                        Details = new MovieDetails
                        {
                            Id = 6,
                            Synopsis = "The life journey of a kind-hearted man who unknowingly influences historical events.",
                            Language = "English",
                            Budget = 55000000
                        },
                        Genres = new List<Genre>
                        {
                            new Genre { Id = 1, Name = "Drama" },
                            new Genre { Id = 8, Name = "Romance" }
                        },
                        Reviews = new List<Review>
                        {
                            new Review { Id = 11, Reviewer = "Emma Thomas", Rating = 5, Comment = "Heartwarming, emotional, and beautifully acted." },
                            new Review { Id = 12, Reviewer = "William Jackson", Rating = 5, Comment = "A touching story that remains relevant decades later." }
                        },
                        Actors = new List<Actor>
                        {
                            new Actor { Id = 16, Name = "Tom Hanks", BirthYear = 1956 },
                            new Actor { Id = 17, Name = "Robin Wright", BirthYear = 1966 },
                            new Actor { Id = 18, Name = "Gary Sinise", BirthYear = 1955 }
                        }
                    },

                    new Movie
                    {
                        Id = 7,
                        Title = "Interstellar",
                        ReleaseYear = 2014,
                        Duration = 169,
                        Details = new MovieDetails
                        {
                            Id = 7,
                            Synopsis = "A team of astronauts travels through a wormhole in search of a new home for humanity.",
                            Language = "English",
                            Budget = 165000000
                        },
                        Genres = new List<Genre>
                        {
                            new Genre { Id = 4, Name = "Science Fiction" },
                            new Genre { Id = 1, Name = "Drama" },
                            new Genre { Id = 7, Name = "Adventure" }
                        },
                        Reviews = new List<Review>
                        {
                            new Review { Id = 13, Reviewer = "Ava White", Rating = 5, Comment = "A visually breathtaking and emotionally powerful film." },
                            new Review { Id = 14, Reviewer = "Benjamin Harris", Rating = 4, Comment = "Ambitious science fiction with memorable performances." }
                        },
                        Actors = new List<Actor>
                        {
                            new Actor { Id = 19, Name = "Matthew McConaughey", BirthYear = 1969 },
                            new Actor { Id = 20, Name = "Anne Hathaway", BirthYear = 1982 },
                            new Actor { Id = 21, Name = "Jessica Chastain", BirthYear = 1977 }
                        }
                    }
                };

                context.Movies.AddRange(movies);
                await context.SaveChangesAsync();
            }
        }
    }
}
