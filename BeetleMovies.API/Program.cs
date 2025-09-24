using AutoMapper;
using BeetleMovies.API.DBContext;
using BeetleMovies.API.Extensions;
using BeetleMovies.API.Profiles;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<BeetleMoviesContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BeetleMovieStr")));

builder.Services.AddAutoMapper(cfg => { }, typeof(BeetleMoviesProfile).Assembly);

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen();

var app = builder.Build();

app.RegisterMoviesEndpoints();
app.RegisterDirectorsEndpoints();


if (!app.Environment.IsDevelopment())
{
  
}
else
{
  app.UseSwagger();
  app.UseSwaggerUI();
}


app.Run();
