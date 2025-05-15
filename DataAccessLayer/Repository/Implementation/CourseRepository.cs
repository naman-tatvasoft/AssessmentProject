using DataAccessLayer.Models;
using DataAccessLayer.Repository.Interface;
using DataAccessLayer.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository.Implementation;

public class CourseRepository : ICourseRepository
{
    private readonly AssessmentProjectDbContext _context;

    public CourseRepository(AssessmentProjectDbContext context)
    {
        _context = context;
    }


    public IQueryable<CourseViewModel> GetCourseList()
    {
        var query = _context.Courses
        .Where(c => !c.isDeleted)
            .Select(x => new CourseViewModel
            {
                Id = x.Id,
                CourseName = x.Name,
                Content = x.Content,
                Credits = x.Credits,
                Department = _context.Departments.FirstOrDefault(c => c.Id == x.DepartmentId).Name,
                isAvailable = x.isAavailable
            }).OrderBy(u => u.CourseName).AsQueryable();
        return query;
    }

    public async Task<bool> AddCourse(CourseViewModel courseVM)
    {

        try
        {
            var course = new Course
            {
                Name = courseVM.CourseName,
                Content = courseVM.Content,
                Credits = courseVM.Credits,
                DepartmentId = courseVM.DepartmentId,
                isAavailable = courseVM.isAvailable
            };
            _context.Add(course);

            return await _context.SaveChangesAsync() > 0;
        }
        catch
        {
            throw;
        }

    }

    public CourseViewModel SetUpdateModal(int id)
    {
        try
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == id);
            if (course != null)
            {
                var courseVM = new CourseViewModel
                {
                    Id = course.Id,
                    CourseName = course.Name,
                    Content = course.Content,
                    Credits = course.Credits,
                    DepartmentId = course.DepartmentId,
                    isAvailable = course.isAavailable
                };
                return courseVM;
            }
            return null;
        }
        catch
        {
            throw;
        }
    }


    public async Task<bool> UpdateCourse(CourseViewModel courseVM)
    {
        try
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == courseVM.Id);

            if (course != null)
            {
                course.Name = courseVM.CourseName;
                course.Content = courseVM.Content;
                course.Credits = courseVM.Credits;
                course.DepartmentId = courseVM.DepartmentId;
                course.isAavailable = courseVM.isAvailable;
                _context.Update(course);
            }

            return await _context.SaveChangesAsync() > 0;
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> DeleteCourse(int id)
    {
        try
        {
            var course = _context.Courses.FirstOrDefault(pr => pr.Id == id);
            if (course != null)
            {
                course.isDeleted = true;
                _context.Update(course);
            }
            return await _context.SaveChangesAsync() > 0;
        }
        catch
        {
            throw;
        }
    }

    public IQueryable<CourseViewModel> GetCourseListStudent(int studentId)
    {
        try
        {
            var query = _context
            .Courses
            .Include(c => c.Department)
            .ThenInclude(c => c.Student)
            .Where(c => !c.isDeleted && c.isAavailable && c.Department.Student.FirstOrDefault().Id == studentId)
                .Select(x => new CourseViewModel
                {
                    Id = x.Id,
                    CourseName = x.Name,
                    Content = x.Content,
                    Credits = x.Credits,
                    Department = _context.Departments.FirstOrDefault(c => c.Id == x.DepartmentId).Name,
                    isAvailable = x.isAavailable
                }).OrderBy(u => u.CourseName).AsQueryable();
            return query;
        }
        catch
        {
            throw;
        }
    }
    public bool IsAlreadyEnrolled(int courseId, int studentId)
    {
        try
        {
            var isEnrolled = _context.Enrollments.Where(en => en.CourseId == courseId && en.StudentId == studentId && !en.isCompleted && !en.isWithdrawn).Any();
            if (isEnrolled)
            {
                return true;
            }
            return false;
        }
        catch
        {
            throw;
        }
    }

    public bool isAnyEnrolled(int courseId)
    {
        try
        {
            var isAnyEnrolled = _context.Enrollments.Where(en => en.CourseId == courseId && !en.isCompleted && !en.isWithdrawn).Any();
            if (isAnyEnrolled)
            {
                return true;
            }
            return false;
        }
        catch
        {
            throw;
        }
    }

    public bool IsAlreadyCompleted(int courseId, int studentId)
    {
        try
        {
            var isCompleted = _context.Enrollments.Where(en => en.CourseId == courseId && en.StudentId == studentId && en.isCompleted).Any();
            if (isCompleted)
            {
                return true;
            }
            return false;
        }
        catch
        {
            throw;
        }
    }



    public async Task<bool> EnrollCourse(int courseId, int studentId)
    {
        try
        {
            var enroll = new Enrollment
            {
                CourseId = courseId,
                StudentId = studentId,
                isCompleted = false,
                isWithdrawn = false
            };
            _context.Add(enroll);
            return await _context.SaveChangesAsync() > 0;
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> WithdrawCourse(int courseId, int studentId)
    {
        try
        {
            var enroll = _context.Enrollments.FirstOrDefault(en => en.CourseId == courseId && en.StudentId == studentId && !en.isCompleted && !en.isWithdrawn);
            if (enroll != null)
            {
                enroll.isWithdrawn = true;
                _context.Update(enroll);
            }
            return await _context.SaveChangesAsync() > 0;
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> CompleteCourse(int courseId, int studentId)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                var enroll = _context.Enrollments.FirstOrDefault(en => en.CourseId == courseId && en.StudentId == studentId && !en.isCompleted && !en.isWithdrawn);
                if (enroll != null)
                {
                    enroll.isCompleted = true;
                    _context.Update(enroll);
                }

                var student = _context.Students.FirstOrDefault(s => s.Id == studentId);
                if (student != null)
                {
                    student.CreditsEarned += _context.Courses.FirstOrDefault(c => c.Id == courseId).Credits;
                    _context.Update(student);

                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
