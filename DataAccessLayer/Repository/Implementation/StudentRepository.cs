using DataAccessLayer.Models;
using DataAccessLayer.Repository.Interface;
using DataAccessLayer.ViewModels;
using Microsoft.EntityFrameworkCore;

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



    public List<StudentHistoryViewModel> GetStudentList(int courseId)
    {

        var studentData = _context.Enrollments
            .Include(en => en.Student)
            .ThenInclude(en => en.UserCred)
            .Where(en => en.CourseId == courseId && !en.isWithdrawn)
            .Select(en => new StudentHistoryViewModel
            {
                Id = en.StudentId,
                Name = en.Student.Name,
                Email = en.Student.UserCred.Email,
                CreditEarned = en.Student.CreditsEarned
            }).ToList();

        return studentData;
    }

     public List<CourseViewModel> GetMyCourses(int studentId)
    {

        var studentData = _context.Enrollments
            .Where(en => en.StudentId == studentId && !en.isWithdrawn && !en.isCompleted)
            .Select(en => new CourseViewModel
            {
                Id = en.CourseId,
                CourseName = en.Course.Name,
                Content = en.Course.Content,
                Credits = en.Course.Credits
            }).ToList();

        return studentData;
    }

}
