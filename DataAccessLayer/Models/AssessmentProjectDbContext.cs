using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models;

public class AssessmentProjectDbContext : DbContext
{
    public AssessmentProjectDbContext(DbContextOptions<AssessmentProjectDbContext> options) : base(options) {}
    public DbSet<Role> Roles {get; set;}
    public DbSet<UserCred> Users {get; set;}

}

// dotnet ef migrations add InitialCreate --project ../DataAccessLayer --startup-project ../AssessmentProject

// dotnet ef database update --project ../DataAccessLayer --startup-project ../AssessmentProject