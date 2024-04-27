using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace _net8_IdentityServer;

public class ApplicationUser : IdentityUser
{
    [Required]
    public string? Name { get; set; }
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
}
