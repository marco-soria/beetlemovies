using AutoMapper;
using BeetleMovies.API.DBContext;
using BeetleMovies.API.Extensions;
using BeetleMovies.API.Profiles;


using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<BeetleMoviesContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BeetleMovieStr")));

builder.Services.AddAutoMapper(cfg => { }, typeof(BeetleMoviesProfile).Assembly);


builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen();

var app = builder.Build();



if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler();

  //Referente of Details that you can do.
  // app.UseExceptionHandler(configureApplicationBuilder =>
  // {
  //   configureApplicationBuilder.Run(async context =>
  //   {
  //     context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
  //     context.Response.ContentType = "text/html";
  //     await context.Response.WriteAsync("An unexpected problem happened.");
  //   });
  // });
}
else
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.RegisterMoviesEndpoints();
app.RegisterDirectorsEndpoints();

app.Run();
