using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

public class Team
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string OwnerId { get; set; }

    [ForeignKey("OwnerId")]
    public AppUser Owner { get; set; }

    public ICollection<AppUser> Members { get; set; }
    public ICollection<Project> Projects { get; set; }
}
