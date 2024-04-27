using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _net8_IdentityServer;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    public AccountsController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    [HttpGet]
    public async Task<IActionResult> GetAccounts()
    {
        var accounts = await _userManager.Users.ToListAsync();
        return Ok(accounts);
    }
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAccount([FromBody] RegistrationDto model)
    {
        Console.WriteLine(model);
        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            Name = model.Name,
            PhoneNumber = model.PhoneNumber
            // Set other properties
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            // Your registration success logic
            return Ok(new { Message = "Registration successful" });
        }

        // If registration fails, return errors
        return BadRequest(new { Errors = result.Errors });
    }
}
