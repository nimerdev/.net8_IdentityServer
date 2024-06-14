using System.ComponentModel.DataAnnotations;

namespace _net8_IdentityServer;

public class RegistrationDto
{
[Required,EmailAddress]
public string? Email { get; set; }
// [Required,RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@^#$%*.~])[a-zA-Z0-9!@#$%*.^~]{12,}$", ErrorMessage =
//  @"Password must be at least 12 characters and contain at least one lower case (a-z), one upper case (A-Z), one digit (0-9) and one special character (e.g. !@#$%^*.~)")]
[Required]
public string? Password { get; set; }
[Required,Compare("Password", ErrorMessage = "Passwords do not match.")]
public string? ConfirmPassword { get; set; }
[Required,RegularExpression($"^[a-zA-Z\\s]+$")]
public string? FullName { get; set; }
[Required]
public string? PhoneNumber { get; set; }
[Required]
public string? OrganizationName { get; set; }
}
