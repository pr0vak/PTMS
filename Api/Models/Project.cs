using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

public class Project
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int TeamId { get; set; }
    [Required]
    public DateTime Deadline { get; set; }

    [ForeignKey("TeamId")]
    public Team Team { get; set; }

    public ICollection<Todo> Todos { get; set; }
}
