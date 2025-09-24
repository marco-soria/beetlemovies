using BeetleMovies.API.DBContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<BeetleMoviesContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BeetleMovieStr")));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
