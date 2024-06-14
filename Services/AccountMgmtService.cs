using System.Security.Cryptography;
using System.Web;
using Microsoft.AspNetCore.Identity;

namespace _net8_IdentityServer;

public class AccountMgmtService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationUser> _roleManager;
    private OrganizationRepository _organziationRepository;
    public AccountMgmtService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationUser> roleManager,
    SignInManager<ApplicationUser> signInManager, OrganizationRepository organziationRepository)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _organziationRepository = organziationRepository;
    }
    public async Task RegisterAccount(RegistrationDto model)
    {
        using (var transaction = _organziationRepository.GetContext().Database.BeginTransaction())
        {
            try
            {
                #region CreatingOrganization
                int count = 1;
                var org = new Organization()
                {
                    IsActive = 1,
                    Name = model.OrganizationName
                };
                //todo: continue!

                var tempOrgName = model.OrganizationName;
                while (await _organziationRepository.GetOrganizationByName(tempOrgName) != null)
                {
                    tempOrgName = string.Format("{0}-{1}", model.OrganizationName, count++);
                }
                org.Name = tempOrgName;
                bool tokenAvailable = false;
                string token = string.Empty;
                while (!tokenAvailable)
                {
                    token = await GenerateToken();
                    tokenAvailable = await CheckIfOrgnizationTokenAvailable(token);
                }
                org.Token = token;
                org = await _organziationRepository.InsertOrganization(org);
                if(org == null)
                {
                    throw new Exception($"Error creating organization {org.Name}");
                }

                #endregion

                #region CreateOrganizationUserAndUser
                
                var newUser = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    Status = UserStatus.Active
                };

                var userManagerResult = await _userManager.CreateAsync(newUser, model.Password);
                if(!userManagerResult.Succeeded)
                {
                    throw new Exception($"Error creating user {model.Email}. {string.Join(",",userManagerResult.Errors.Select(x => x.Description))}");
                }

                var orgUser = new OrganizationUser
                {
                    OrganizationId = org.OrganizationId,
                    UserId = newUser.Id,
                    OrganizationUserStatus = OrganizationUserStatus.Active,
                    DateJoined = DateTime.UtcNow
                };

                //todo: add inserting orgUser and OrgUserRepository

                #endregion


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

    public async Task<string> GenerateToken()
    {
        var randomNumber = new byte[128];
        string token = string.Empty;
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            token = Convert.ToBase64String(randomNumber).Replace("+", "").Replace("/", "");
            token = HttpUtility.UrlEncode(token);
            token = token.Substring(0, 128);
        }
        return token;
    }


    private async Task<bool> CheckIfOrgnizationTokenAvailable(string token)
    {
        var org = await _organziationRepository.GetOrganizationByTokenAsNoTracking(token);
        if (org == null)
        {
            return true;
        }
        return false;
    }

}
