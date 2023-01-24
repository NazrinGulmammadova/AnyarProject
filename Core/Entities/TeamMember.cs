using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class TeamMember
{
    public int Id { get; set; }
    [Required,MaxLength(50)]
    public string? Name { get; set; }
    [Required, MaxLength(100)]
    public string? Position { get; set; }
    [Required, MaxLength(250)]
    public string? Description { get; set; }
    [Required, MaxLength(100)]
    public string? Image { get; set; }
}
