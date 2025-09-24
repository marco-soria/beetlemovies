using AutoMapper;
using BeetleMovies.API.DBContext;
using BeetleMovies.API.DTOs;
using BeetleMovies.API.Properties;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<BeetleMoviesContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BeetleMovieStr")));

builder.Services.AddAutoMapper(cfg => { }, typeof(BeetleMoviesProfile).Assembly);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/moviesalone/{number}", (int number) => $"Best Movies API {number}"); // parameter must be in parentheses

app.MapGet("/movies/{number:int}", async (
  BeetleMoviesContext context,
  IMapper mapper,
  int number
) =>
{
  return mapper.Map<MovieDTO>(await context.Movies.FirstOrDefaultAsync(x => x.Id == number));
});

app.MapGet("/movies/{moviesId:int}/directors", async (
  BeetleMoviesContext context,
  IMapper mapper,
  int moviesId
) =>
{
  return mapper.Map<IEnumerable<DirectorDTO>>((await context.Movies
                      .Include(movie => movie.Directors)
                      .FirstOrDefaultAsync(movie => movie.Id == moviesId))?.Directors);
});

app.MapGet("/movies", async Task<Results<NoContent, Ok<IEnumerable<MovieDTO>>>> (
  BeetleMoviesContext context,
  IMapper mapper,
  [FromQuery(Name = "movieName")] string? title
) =>
{
  var movieEntity = await context.Movies
                                 .Where(x => title == null || x.Title.ToLower().Contains(title.ToLower()))
                                 .ToListAsync();

  if (movieEntity.Count <= 0 || movieEntity == null)
    return TypedResults.NoContent();
  else
    return TypedResults.Ok(mapper.Map<IEnumerable<MovieDTO>>(movieEntity));
});

app.Run();
