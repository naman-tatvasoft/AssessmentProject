using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models;

public class AssessmentProjectDbContext : DbContext
{
    public AssessmentProjectDbContext(DbContextOptions<AssessmentProjectDbContext> options) : base(options) {}
    public DbSet<UserCred> Users {get; set;}

}
