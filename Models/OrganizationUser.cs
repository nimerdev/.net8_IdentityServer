using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _net8_IdentityServer;

public class OrganizationUser
{
    [Key]
    public int OrganizationUserID { get; set; }
    public int OrganizationId { get; set; }
    public Organization Organization { get; set; }
    [MaxLength(450)]
    public string UserId { get; set; }
    [ForeignKey("OrganizationUserStatus")]
    public int StatusId { get; set; }
    public OrganizationUserStatus OrganizationUserStatus { get; set; }
    public DateTime DateJoined { get; set; }
    // [ForeignKey("ApplicationRole")]
    // public int RoleId { get; set; }
    // public ApplicationRole ApplicationRole { get; set; }
    public DateTime? LastSignInDate { get; set; }
    public DateTime? LastActivityDate { get; set; }
    public string? InvitedByUserId { get; set; }
    [MaxLength(128)]
    public string? InvitationToken { get; set; }
    public bool IsBetaTester { get; set; }
    public int ColorId { get; set; }
    public string? LastLocationUrl { get; set; }
    public bool Is2FAApplied { get; set; }
    public DateTime? Date2FAApplied { get; set; }
    public Validation2FATypes Validation2FAType { get; set; }
    [MaxLength(10)]
    public string? ValidationCode { get; set; }
    public DateTime? ValidationCodeExpirationDate { get; set; }
    public int ValidationCodeAttempts { get; set; }

    public enum Validation2FATypes
    {
        Email = 0,
        SMS = 1,
        Application = 2
    }
}

public class OrganizationUserStatus
{
    [Key]
    public int StatusId { get; set; }
    [MaxLength(50)]
    public string StatusName { get; set; }
    [MaxLength(100)]
    public string StatusDesc { get; set; }
    public bool IsInitial { get; set; }
    public bool IsActive { get; set; }
    public bool IsPending { get; set; }
    public bool IsNotActive { get; set; }
    public bool IsDeleted { get; set; }
}
