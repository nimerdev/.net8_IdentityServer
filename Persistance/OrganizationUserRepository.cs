using Microsoft.EntityFrameworkCore;

namespace _net8_IdentityServer;

public partial class OrganizationRepository
{
    public async Task<OrganizationUser> InsertOrganizationUser(OrganizationUser orgUser)
    {
        _dbContext.OrganizationUsers.Add(orgUser);
        await _dbContext.SaveChangesAsync();
        return orgUser;
    }

    public async Task<List<OrganizationUser>> GetUserOrganizationsByUserId(string UserId)
    {
        return await _dbContext.OrganizationUsers
            .Include(a => a.Organization)
            .Where(ou => ou.UserId == UserId && ou.OrganizationUserStatus == OrganizationUserStatus.Active
             && ou.Organization.IsActive == 1)
            .ToListAsync();
    }

    public async Task<List<OrganizationUser>> GetOrganizationUsersByOrganzationToken(int orgId)
    {
        return await _dbContext.OrganizationUsers
        .Where(ou => ou.OrganizationId == orgId)
        .ToListAsync();

    }

}
