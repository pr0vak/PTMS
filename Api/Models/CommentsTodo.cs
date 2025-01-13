using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

public class CommentsTodo
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string OwnerId { get; set; }
    [Required]
    public int TodoId { get; set; }
    [Required]
    public string Message { get; set; }
    public string NewStatus { get; set; }
    public string OldStatus { get; set; }

    [ForeignKey("OwnerId")]
    public AppUser Owner { get; set; }
    [ForeignKey("TodoId")]
    public Todo Todo { get; set; }
}