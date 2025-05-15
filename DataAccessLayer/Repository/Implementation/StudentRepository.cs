using DataAccessLayer.Models;
using DataAccessLayer.Repository.Interface;
using DataAccessLayer.ViewModels;

namespace DataAccessLayer.Repository.Implementation;

public class StudentRepository : IStudentRepository
{
    private readonly AssessmentProjectDbContext _context;

    public StudentRepository(AssessmentProjectDbContext context)
    {
        _context = context;
    }

    public async Task<Student> RegisterStudent(RegisterViewModel registerData)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                var user = new UserCred
                {
                    Email = registerData.Email,
                    RoleId = 1,
                    Password = registerData.Password
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var student = new Student
                {
                    Name = registerData.Name,
                    UserCredId = user.Id,
                    DepartmentId = registerData.DepartmentId,
                    CreditsEarned = 0
                };
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                
                await transaction.CommitAsync();

                return student;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
