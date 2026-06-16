using LexiconMovieApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LexiconMovieApi.Data.Seed
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(MovieDbContext context)
        {
            await context.Database.MigrateAsync();

            List<Genre> genres;

            if (!await context.Genres.AnyAsync())
            {
                genres = new List<Genre>
                {
                    new Genre { Name = "Drama" },
                    new Genre { Name = "Action" },
                    new Genre { Name = "Crime" },
                    new Genre { Name = "Science Fiction" },
                    new Genre { Name = "Thriller" },
                    new Genre { Name = "Fantasy" },
                    new Genre { Name = "Adventure" },
                    new Genre { Name = "Romance" }
                };

                context.Genres.AddRange(genres);
                await context.SaveChangesAsync();
            }
            else
            {
                genres = await context.Genres.ToListAsync();
            }

            if (!await context.Movies.AnyAsync())
            {
                var movies = new List<Movie>
                {
                    new Movie
                    {
                        Title = "The Shawshank Redemption",
                        ReleaseYear = 1994,
                        Duration = 142,
                        Details = new MovieDetails
                        {
                            Synopsis = "Two imprisoned men bond over decades while finding hope and redemption behind prison walls.",
                            Language = "English",
                            Budget = 25000000
                        },
                        Genres = new List<Genre>
                        {
                            genres.First(g => g.Name == "Drama")
                        },
                        Reviews = new List<Review>
                        {
                            new Review { Reviewer = "Emily Carter", Rating = 5, Comment = "An inspiring story with unforgettable performances." },
                            new Review { Reviewer = "Michael Thompson", Rating = 5, Comment = "A timeless masterpiece about hope and friendship." }
                        },
                        Actors = new List<Actor>
                        {
                            new Actor { Name = "Tim Robbins", BirthYear = 1958 },
                            new Actor { Name = "Morgan Freeman", BirthYear = 1937 },
                            new Actor { Name = "Bob Gunton", BirthYear = 1945 }
                        }
                    },

                    new Movie
                    {
                        Title = "The Dark Knight",
                        ReleaseYear = 2008,
                        Duration = 152,
                        Details = new MovieDetails
                        {
                            Synopsis = "Batman faces the Joker, a criminal mastermind determined to plunge Gotham City into chaos.",
                            Language = "English",
                            Budget = 185000000
                        },
                        Genres = new List<Genre>
                        {
                            genres.First(g => g.Name == "Action"),
                            genres.First(g => g.Name == "Crime"),
                            genres.First(g => g.Name == "Drama")
                        },
                        Reviews = new List<Review>
                        {
                            new Review { Reviewer = "Sarah Johnson", Rating = 5, Comment = "A gripping superhero film elevated by an iconic villain." },
                            new Review { Reviewer = "David Wilson", Rating = 5, Comment = "Exceptional action, storytelling, and performances." }
                        },
                        Actors = new List<Actor>
                        {
                            new Actor { Name = "Christian Bale", BirthYear = 1974 },
                            new Actor { Name = "Heath Ledger", BirthYear = 1979 },
                            new Actor { Name = "Aaron Eckhart", BirthYear = 1968 }
                        }
                    },

                    new Movie
                    {
                        Title = "Inception",
                        ReleaseYear = 2010,
                        Duration = 148,
                        Details = new MovieDetails
                        {
                            Synopsis = "A skilled thief enters people's dreams to steal secrets and is offered a chance at redemption.",
                            Language = "English",
                            Budget = 160000000
                        },
                        Genres = new List<Genre>
                        {
                            genres.First(g => g.Name == "Science Fiction"),
                            genres.First(g => g.Name == "Thriller"),
                            genres.First(g => g.Name == "Action")
                        },
                        Reviews = new List<Review>
                        {
                            new Review { Reviewer = "Jessica Miller", Rating = 5, Comment = "A mind-bending and visually stunning experience." },
                            new Review { Reviewer = "Christopher Brown", Rating = 4, Comment = "Complex, ambitious, and highly rewarding." }
                        },
                        Actors = new List<Actor>
                        {
                            new Actor { Name = "Leonardo DiCaprio", BirthYear = 1974 },
                            new Actor { Name = "Joseph Gordon-Levitt", BirthYear = 1981 },
                            new Actor { Name = "Tom Hardy", BirthYear = 1977 }
                        }
                    },

                    new Movie
                    {
                        Title = "The Lord of the Rings: The Fellowship of the Ring",
                        ReleaseYear = 2001,
                        Duration = 178,
                        Details = new MovieDetails
                        {
                            Synopsis = "A young hobbit begins a perilous journey to destroy a powerful ring.",
                            Language = "English",
                            Budget = 93000000
                        },
                        Genres = new List<Genre>
                        {
                            genres.First(g => g.Name == "Fantasy"),
                            genres.First(g => g.Name == "Adventure")
                        },
                        Reviews = new List<Review>
                        {
                            new Review { Reviewer = "Olivia Davis", Rating = 5, Comment = "An epic fantasy adventure that set a new standard." },
                            new Review { Reviewer = "Daniel Martinez", Rating = 5, Comment = "Immersive world-building and unforgettable characters." }
                        },
                        Actors = new List<Actor>
                        {
                            new Actor { Name = "Elijah Wood", BirthYear = 1981 },
                            new Actor { Name = "Ian McKellen", BirthYear = 1939 },
                            new Actor { Name = "Viggo Mortensen", BirthYear = 1958 }
                        }
                    },

                    new Movie
                    {
                        Title = "Pulp Fiction",
                        ReleaseYear = 1994,
                        Duration = 154,
                        Details = new MovieDetails
                        {
                            Synopsis = "Interconnected stories of crime, violence, and redemption unfold in Los Angeles.",
                            Language = "English",
                            Budget = 8000000
                        },
                        Genres = new List<Genre>
                        {
                            genres.First(g => g.Name == "Crime"),
                            genres.First(g => g.Name == "Thriller")
                        },
                        Reviews = new List<Review>
                        {
                            new Review { Reviewer = "Sophia Anderson", Rating = 5, Comment = "Sharp dialogue and unforgettable storytelling." },
                            new Review { Reviewer = "James Taylor", Rating = 4, Comment = "Bold, stylish, and endlessly quotable." }
                        },
                        Actors = new List<Actor>
                        {
                            new Actor { Name = "John Travolta", BirthYear = 1954 },
                            new Actor { Name = "Samuel L. Jackson", BirthYear = 1948 },
                            new Actor { Name = "Uma Thurman", BirthYear = 1970 }
                        }
                    },

                    new Movie
                    {
                        Title = "Forrest Gump",
                        ReleaseYear = 1994,
                        Duration = 142,
                        Details = new MovieDetails
                        {
                            Synopsis = "The life journey of a kind-hearted man who unknowingly influences historical events.",
                            Language = "English",
                            Budget = 55000000
                        },
                        Genres = new List<Genre>
                        {
                            genres.First(g => g.Name == "Drama"),
                            genres.First(g => g.Name == "Romance")
                        },
                        Reviews = new List<Review>
                        {
                            new Review { Reviewer = "Emma Thomas", Rating = 5, Comment = "Heartwarming, emotional, and beautifully acted." },
                            new Review { Reviewer = "William Jackson", Rating = 5, Comment = "A touching story that remains relevant decades later." }
                        },
                        Actors = new List<Actor>
                        {
                            new Actor { Name = "Tom Hanks", BirthYear = 1956 },
                            new Actor { Name = "Robin Wright", BirthYear = 1966 },
                            new Actor { Name = "Gary Sinise", BirthYear = 1955 }
                        }
                    },

                    new Movie
                    {
                        Title = "Interstellar",
                        ReleaseYear = 2014,
                        Duration = 169,
                        Details = new MovieDetails
                        {
                            Synopsis = "A team of astronauts travels through a wormhole in search of a new home for humanity.",
                            Language = "English",
                            Budget = 165000000
                        },
                        Genres = new List<Genre>
                        {
                            genres.First(g => g.Name == "Science Fiction"),
                            genres.First(g => g.Name == "Adventure"),
                            genres.First(g => g.Name == "Drama")
                        },
                        Reviews = new List<Review>
                        {
                            new Review { Reviewer = "Ava White", Rating = 5, Comment = "A visually breathtaking and emotionally powerful film." },
                            new Review { Reviewer = "Benjamin Harris", Rating = 4, Comment = "Ambitious science fiction with memorable performances." }
                        },
                        Actors = new List<Actor>
                        {
                            new Actor { Name = "Matthew McConaughey", BirthYear = 1969 },
                            new Actor { Name = "Anne Hathaway", BirthYear = 1982 },
                            new Actor { Name = "Jessica Chastain", BirthYear = 1977 }
                        }
                    }
                };

                context.Movies.AddRange(movies);
                await context.SaveChangesAsync();
            }
        }
    }
}
