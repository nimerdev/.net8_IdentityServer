namespace _net8_IdentityServer;

public class AccountRepository
{
    private ApplicationDbContext _dbContext;
    public AccountRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ApplicationDbContext GetContext() => _dbContext;
}
