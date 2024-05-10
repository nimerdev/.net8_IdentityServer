using System.ComponentModel.DataAnnotations;

namespace _net8_IdentityServer;

public class Organization
{
    [Key]
    public int OrganizationId { get; set; }
    [MaxLength(128)]
    public string Token { get; set; } = Guid.NewGuid().ToString();
    public int IsActive { get; set; } = 0;
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(50)]
    public string? Country { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [MaxLength(100)]
    public string? Logo { get; set; } = string.Empty;
    public bool Is2FAEnabled { get; set; }
    public bool IsIPFilterEnabled { get; set; }
    public bool IsSessionTimeoutEnabled { get; set; }
    public int SessionTimeOut { get; set; }
    [MaxLength(100)]
    public string? LocalTimeZone { get; set; }
}
