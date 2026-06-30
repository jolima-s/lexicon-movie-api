using Asp.Versioning;
using LexiconMovieApi.Core.DomainContracts;
using LexiconMovieApi.Core.Entities;
using LexiconMovieApi.Data;
using LexiconMovieApi.Data.Contracts;
using LexiconMovieApi.Data.Repositories;
using LexiconMovieApi.Data.Seed;
using LexiconMovieApi.Data.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // 1. Detta är oförändrat och definierar Bearer-knappen
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT-1234567890abcdefghijklmnopqrstuvxz"
    });

    // 2. DETTA ÄR NYTT FÖR .NET 10: Vi använder en lambda-funktion med det nya schemareferensobjektet
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
    });
});

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]!);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();


builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

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
    app.UseSwagger();
    app.UseSwaggerUI();
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
