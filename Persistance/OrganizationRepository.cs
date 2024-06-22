using Microsoft.EntityFrameworkCore;

namespace _net8_IdentityServer;

public partial class OrganizationRepository
{
    private ApplicationDbContext _dbContext;
    public OrganizationRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ApplicationDbContext GetContext() => _dbContext;

    public async Task<Organization> GetOrganizationByName(string name)
    {
        return await _dbContext.Organizations
        .FirstOrDefaultAsync(o => o.Name == name);
    }

    public async Task<Organization> GetOrganizationByTokenAsNoTracking(string token)
    {
        return await _dbContext.Organizations
        .AsNoTracking()
        .FirstOrDefaultAsync(o => o.Token == token);
    }

    public async Task<Organization> InsertOrganization(Organization organization)
    {
        _dbContext.Organizations.Add(organization);
        await _dbContext.SaveChangesAsync();

        return organization;
    }
}
