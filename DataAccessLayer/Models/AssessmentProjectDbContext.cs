using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models;

public class AssessmentProjectDbContext : DbContext
{
    public AssessmentProjectDbContext(DbContextOptions<AssessmentProjectDbContext> options) : base(options) {}
    public DbSet<Role> Roles {get; set;}
    public DbSet<UserCred> Users {get; set;}
    public DbSet<Department> Departments {get; set;}
    public DbSet<Course> Courses {get; set;}
    public DbSet<Student> Students {get; set;}
    public DbSet<Enrollment> Enrollments {get; set;}
}

// dotnet ef migrations add InitialCreate --project ../DataAccessLayer --startup-project ../AssessmentProject

// dotnet ef database update --project ../DataAccessLayer --startup-project ../AssessmentProject