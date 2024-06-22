using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _net8_IdentityServer;

[Route("api/[controller]")]
[ApiController]
public class OrganizationsController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly OrgMgmtService _orgMgmtService;
    public OrganizationsController(UserManager<ApplicationUser> userManager, OrgMgmtService orgMgmtService)
    {
        _userManager = userManager;
        _orgMgmtService = orgMgmtService;
    }
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userManager.Users.ToListAsync();
        return Ok(users);
    }
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterOrganziationAndUser([FromBody] RegistrationDto model)
    {
        Console.WriteLine(model);

        try
        {
            var result = await _orgMgmtService.RegisterOrganziationAndUser(model);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        try
        {
            return Ok(await _orgMgmtService.Login(model));
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}

