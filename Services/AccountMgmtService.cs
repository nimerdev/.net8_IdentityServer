using Microsoft.AspNetCore.Identity;

namespace _net8_IdentityServer;

public class AccountMgmtService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationUser> _roleManager;
    private AccountRepository _accountRepository;
    public AccountMgmtService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationUser> roleManager,
    SignInManager<ApplicationUser> signInManager, AccountRepository accountRepository)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _accountRepository = accountRepository;
    }
    public async Task RegisterAccount(RegistrationDto model)
    {
        using (var transaction = _accountRepository.GetContext().Database.BeginTransaction())
        {
            try
            {                
                int count = 1;
                var org = new Organization()
                {
                    IsActive = 1,
                    Name = model.OrganizationName
                };
                //todo: continue!
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public async Task<bool> Login(LoginDto model)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                throw new UnauthorizedAccessException("User doesn't exist");

            var signInManagerResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, lockoutOnFailure: false);
            if (!signInManagerResult.Succeeded)
                throw new UnauthorizedAccessException($"Unable to sign in: {model.Email}");
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
