using LexiconMovieApi.Client.Controllers;
using LexiconMovieApi.Core.DTOs.Actor;
using LexiconMovieApi.Core.DTOs.Genre;
using LexiconMovieApi.Core.DTOs.Movie;
using LexiconMovieApi.Core.DTOs.Review;
using LexiconMovieApi.Core.Entities;
using LexiconMovieApi.Data.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LexiconMovieApi.Tests.Controllers;

public class MoviesControllerTests
{
    [Fact]
    public async Task Get_Movies_ReturnsOkResult()
    {
        // Arrange
        var mockServiceManager = new Mock<IServiceManager>();
        mockServiceManager.Setup(service => service.MovieService.GetAllMoviesAsync())
            .ReturnsAsync(new List<MovieDto> { new MovieDto { Id = 1, Title = "Test Movie" } });
        var controller = new MoviesController(mockServiceManager.Object);

        // Act
        var result = await controller.GetMovies();

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<MovieDto>>(okObjectResult.Value);
        Assert.Single(returnValue);
    }

    [Fact]
    public async Task Get_MovieById_ReturnsOkResult()
    {
        // Arrange
        var mockServiceManager = new Mock<IServiceManager>();
        mockServiceManager.Setup(service => service.MovieService.GetMovieByIdAsync(1))
            .ReturnsAsync(new MovieDto { Id = 1, Title = "Test Movie" });
        var controller = new MoviesController(mockServiceManager.Object);

        // Act
        var result = await controller.GetMovie(1);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<MovieDto>(okObjectResult.Value);
        Assert.Equal(1, returnValue.Id);
    }

    [Fact]
    public async Task Get_MovieById_ReturnsNotFoundResult()
    {
        // Arrange
        var mockServiceManager = new Mock<IServiceManager>();
        mockServiceManager.Setup(service => service.MovieService.GetMovieByIdAsync(1))
            .ReturnsAsync((MovieDto?)null);
        var controller = new MoviesController(mockServiceManager.Object);

        // Act
        var result = await controller.GetMovie(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Get_MovieWithDetails_ReturnsOkResult()
    {
        // Arrange
        var mockServiceManager = new Mock<IServiceManager>();
        mockServiceManager.Setup(service => service.MovieService.GetMovieWithDetailsAsync(1, true, true, true))
            .ReturnsAsync(new MovieDetailedDto 
            { 
                Id = 1, 
                Title = "Test Movie",
                ReleaseYear = 2020,
                Duration = 120,
                Details = new MovieDetailsDto
                {
                    Synopsis = "Test synopsis",
                    Language = "English",
                    Budget = 1000000
                },
                Genres = new List<GenreDto> { new GenreDto { Id = 1, Name = "Action" } },
                Reviews = new List<ReviewDto> { new ReviewDto { Id = 1, Comment = "Great movie!", Rating = 5 } },
                Actors = new List<ActorDto> { new ActorDto { Id = 1, Name = "Actor 1", BirthYear = 1980 } }
            });
        var controller = new MoviesController(mockServiceManager.Object);

        // Act
        var result = await controller.GetMovieWithDetails(1, true, true, true);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<MovieDetailedDto>(okObjectResult.Value);
        Assert.Equal(1, returnValue.Id);
    }

    [Fact]
    public async Task Post_Movie_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var mockServiceManager = new Mock<IServiceManager>();
        var mockMovieService = new Mock<IMovieService>();
        mockServiceManager.Setup(service => service.MovieService).Returns(mockMovieService.Object);
        mockServiceManager.Setup(service => service.MovieService.CreateMovieAsync(It.IsAny<MovieCreateDto>()))
            .ReturnsAsync(new MovieDto { Id = 1, Title = "Test Movie" });
        var controller = new MoviesController(mockServiceManager.Object);

        // Act
        var result = await controller.PostMovie(new MovieCreateDto { Title = "Test Movie" });

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<MovieDto>(createdAtActionResult.Value);
        Assert.Equal(1, returnValue.Id);
    }

    [Fact]
    public async Task Post_Movie_ReturnsBadRequest_WhenValidationFails()
    {
        // Arrange
        var mockServiceManager = new Mock<IServiceManager>();
        var mockMovieService = new Mock<IMovieService>();
        mockServiceManager.Setup(service => service.MovieService).Returns(mockMovieService.Object);
        var controller = new MoviesController(mockServiceManager.Object);

        // ModelState is never populated by the real pipeline in unit tests,
        // so we must manually add validation errors to simulate [Range] failure.
        controller.ModelState.AddModelError(nameof(MovieCreateDto.ReleaseYear), "The field ReleaseYear must be between 1900 and 2100.");

        // Act
        var result = await controller.PostMovie(new MovieCreateDto
        {
            Title = "Test Movie",
            ReleaseYear = 1877, // Invalid year
            Duration = 120
        });

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task Put_Movie_ReturnsNoContentResult()
    {
        // Arrange
        var mockServiceManager = new Mock<IServiceManager>();
        var mockMovieService = new Mock<IMovieService>();
        mockServiceManager.Setup(service => service.MovieService).Returns(mockMovieService.Object);
        var controller = new MoviesController(mockServiceManager.Object);
        var movieUpdateDto = new MovieUpdateDto 
        { 
            Id = 1, 
            Title = "Updated Movie",
            ReleaseYear = 2020,
            Duration = 120,
            Details = new MovieDetails 
            { 
                Synopsis = "Updated description",
                Language = "English",
                Budget = 1000000
            },
            Actors = new List<ActorDto>()
            { 
                new ActorDto { Id = 1, Name = "Actor 1", BirthYear = 1980 },
                new ActorDto { Id = 2, Name = "Actor 2", BirthYear = 1990 }
            }
        };

        // Act
        var result = await controller.PutMovie(1, movieUpdateDto);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Put_Movie_ReturnsBadRequest_WhenIdMismatch()
    {
        // Arrange
        var mockServiceManager = new Mock<IServiceManager>();
        var mockMovieService = new Mock<IMovieService>();
        mockServiceManager.Setup(service => service.MovieService).Returns(mockMovieService.Object);
        var controller = new MoviesController(mockServiceManager.Object);
        var movieUpdateDto = new MovieUpdateDto
        {
            Id = 1,
            Title = "Updated Movie",
            ReleaseYear = 2020,
            Duration = 120,
            Details = new MovieDetails { Synopsis = "Updated description" },
            Actors = new List<ActorDto>()
            {
                new ActorDto { Id = 1, Name = "Actor 1", BirthYear = 1980 },
                new ActorDto { Id = 2, Name = "Actor 2", BirthYear = 1990 }
            }
        };

        // Act
        var result = await controller.PutMovie(2, movieUpdateDto); // Mismatched ID

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Put_Movie_ReturnsNotFound_WhenMovieDoesNotExist()
    {
        // Arrange
        var mockServiceManager = new Mock<IServiceManager>();
        var mockMovieService = new Mock<IMovieService>();
        mockServiceManager.Setup(service => service.MovieService).Returns(mockMovieService.Object);
        mockMovieService.Setup(service => service.UpdateMovieAsync(It.IsAny<MovieUpdateDto>()))
            .ThrowsAsync(new KeyNotFoundException("Movie not found."));
        var controller = new MoviesController(mockServiceManager.Object);
        var movieUpdateDto = new MovieUpdateDto
        {
            Id = 1,
            Title = "Updated Movie",
            ReleaseYear = 2020,
            Duration = 120,
            Details = new MovieDetails { Synopsis = "Updated description" },
            Actors = new List<ActorDto>()
            {
                new ActorDto { Id = 1, Name = "Actor 1", BirthYear = 1980 },
                new ActorDto { Id = 2, Name = "Actor 2", BirthYear = 1990 }
            }
        };
        // Act
        var result = await controller.PutMovie(1, movieUpdateDto);
        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}