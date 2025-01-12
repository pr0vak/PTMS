using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

public class Todo
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Priority { get; set; }
    [Required]
    public string Status { get; set; }
    [Required]
    public string AssigneeId { get; set; }
    [Required]
    public int ProjectId { get; set; }
    [Required]
    public DateTime Deadline { get; set; }

    [ForeignKey("AssigneeId")]
    public AppUser Assignee { get; set; }
    [ForeignKey("ProjectId")]
    public Project Project { get; set; }
}