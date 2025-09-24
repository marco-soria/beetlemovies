using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BeetleMovies.API.Entities;

public class Director
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    public ICollection<Movie> Movies { get; set; } = [];

    public Director()
    {

    }
    [SetsRequiredMembers]
    public Director(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
