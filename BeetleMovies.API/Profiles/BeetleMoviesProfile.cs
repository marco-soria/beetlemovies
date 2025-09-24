using AutoMapper;
using BeetleMovies.API.DTOs;
using BeetleMovies.API.Entities;

namespace BeetleMovies.API.Profiles;

public class BeetleMoviesProfile : Profile
{
  public BeetleMoviesProfile()
  {
    CreateMap<Movie, MovieDTO>().ReverseMap();
    CreateMap<Movie, MovieForCreatingDTO>().ReverseMap();
    CreateMap<Movie, MovieForUpdatingDTO>().ReverseMap();
    CreateMap<Director, DirectorDTO>() //
      .ForMember(d => d.MovieId, // here we are mapping the MovieId property of DirectorDTO
                 o => o.MapFrom(d => d.Movies.First().Id)); // here we are getting the first Movie's Id from the Movies collection in Director entity and mapping it to MovieId, this is because Director entity has a collection of Movies this allows us to get the MovieId directly
  }
}
