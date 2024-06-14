using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace _net8_IdentityServer;

public class ApplicationUser : IdentityUser
{
    [Required]
    public string? FullName { get; set; }
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
    public UserStatus Status { get; set; }
}

public enum UserStatus
{
    Active = 1,
    Inactive = -1,
}
