using AutoMapper;
using BeetleMovies.API.DBContext;
using BeetleMovies.API.Extensions;
using BeetleMovies.API.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<BeetleMoviesContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BeetleMovieStr")));

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
  .AddEntityFrameworkStores<BeetleMoviesContext>();

builder.Services.AddAutoMapper(cfg => { }, typeof(BeetleMoviesProfile).Assembly);


builder.Services.AddProblemDetails();

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

builder.Services.AddAuthorizationBuilder()
  .AddPolicy("RequireAdminFromPeru", policy => 
    policy
      .RequireRole("admin")
      .RequireClaim("country", "Peru"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
  options.AddSecurityDefinition("TakenAuthMovies",
    new() 
    {
      Name = "Authorization",
      Description = "Token based on Authorization and Authentication",
      Type = SecuritySchemeType.Http,
      Scheme = "Bearer",
      In = ParameterLocation.Header
    }
  );
  options.AddSecurityRequirement(new() 
  {
      {
        new() {
          Reference = new OpenApiReference {
            Type = ReferenceType.SecurityScheme,
            Id = "TakenAuthMovies"
          }
        }, new List<string>()
      }
    }
  );
});


builder.Services.AddSwaggerGen();

var app = builder.Build();



if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler();

  //Reference of Details that you can do.
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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.RegisterMoviesEndpoints();
app.RegisterDirectorsEndpoints();

app.Run();
