using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _net8_IdentityServer;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Organization> Accounts { get; set; }
    public DbSet<OrganizationUser> AccountUsers { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
