using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace WebApplication1.Areas.AnyarAdmin.ViewModels.Team;

public class TeamMemberVM
{

    [Required, MaxLength(50)]
    public string? Name { get; set; }
    [Required, MaxLength(100)]
    public string? Position { get; set; }
    [Required, MaxLength(250)]
    public string? Description { get; set; }
    [Required]
    public IFormFile Image { get; set; }
}
