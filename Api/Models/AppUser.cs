using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Api.Models;

public class AppUser : IdentityUser
{
    [Required]
    public string FullName { get; set; }
    public int? TeamId { get; set; }

    [ForeignKey("TeamId")]
    public Team Team { get; set; }

    public ICollection<Todo> Todos { get; set; }
}
