using LexiconMovieApi.Core.Entities;
using LexiconMovieApi.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace LexiconMovieApi.Data;

public class MovieDbContext : DbContext, IMovieDbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options)
    : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;
    public DbSet<MovieDetails> MoviesDetails { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<Actor> Actors { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the Movie entity
        modelBuilder.Entity<Movie>().Property(m => m.Title).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Movie>().ToTable(t => 
            {
                t.HasCheckConstraint("CK_Movie_ReleaseYear", "ReleaseYear >= 1900 AND ReleaseYear <= 2100");
                t.HasCheckConstraint("CK_Movie_Duration", "Duration >= 0 AND Duration <= 1000000");
            });

        modelBuilder.Entity<Movie>()
            .HasOne(m => m.Details)
            .WithOne(md => md.Movie)
            .HasForeignKey<MovieDetails>(md => md.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Genres)
            .WithMany(g => g.Movies)
            .UsingEntity(mg => mg.ToTable("MovieGenres"));

        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Reviews)
            .WithOne(r => r.Movie)
            .HasForeignKey(r => r.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Actors)
            .WithMany(a => a.Movies)
            .UsingEntity(ma => ma.ToTable("MovieActors"));

        // Configure the Genre entity
        modelBuilder.Entity<Genre>().Property(g => g.Name).IsRequired().HasMaxLength(50);

        // Configure the MovieDetails entity
        modelBuilder.Entity<MovieDetails>().Property(md => md.Synopsis).IsRequired().HasMaxLength(1000);
        modelBuilder.Entity<MovieDetails>().Property(md => md.Language).IsRequired().HasMaxLength(30);
        modelBuilder.Entity<MovieDetails>().Property(md => md.Budget).HasPrecision(18, 2);

        // Configure the Review entity
        modelBuilder.Entity<Review>().Property(r => r.Reviewer).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Review>().Property(r => r.Comment).IsRequired().HasMaxLength(1000);
        modelBuilder.Entity<Review>().Property(r => r.Rating).IsRequired();
        modelBuilder.Entity<Review>().ToTable(t => t.HasCheckConstraint("CK_Review_Rating", "Rating >= 1 AND Rating <= 5"));

        // Configure the Actor entity
        modelBuilder.Entity<Actor>().Property(a => a.Name).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Actor>().Property(a => a.BirthYear).IsRequired();
        modelBuilder.Entity<Actor>().ToTable(t => t.HasCheckConstraint("CK_Actor_BirthYear", "BirthYear >= 1900 AND BirthYear <= 2100"));
    }
}