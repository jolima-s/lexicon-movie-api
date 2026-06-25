using LexiconMovieApi.Core.DomainContracts;
using LexiconMovieApi.Core.Entities;
using LexiconMovieApi.Data;
using LexiconMovieApi.Data.Contracts;
using LexiconMovieApi.Data.Repositories;
using LexiconMovieApi.Data.Seed;
using LexiconMovieApi.Data.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAutoMapper(
    cfg => { },
    AppDomain.CurrentDomain.GetAssemblies()
);

var connectionString = builder.Configuration.GetConnectionString("MovieDbConnection");
builder.Services.AddDbContext<IMovieDbContext, MovieDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IRepository<Genre>, GenreRepository>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<IRepository<Review>, ReviewRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IActorService, ActorService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
    await DbSeeder.SeedAsync(context);
}

app.Run();
