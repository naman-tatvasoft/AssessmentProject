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

    public CourseViewModel SetUpdateModal(int id)
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


    public async Task<bool> UpdateCourse(CourseViewModel courseVM)
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

    public async Task<bool> DeleteCourse(int id)
    {
        var course = _context.Courses.FirstOrDefault(pr => pr.Id == id);
        if (course != null)
        {
            course.isDeleted = true;
            _context.Update(course);
        }
        return await _context.SaveChangesAsync() > 0;
    }

    public IQueryable<CourseViewModel> GetCourseListStudent(int studentId)
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
    public bool IsAlreadyEnrolled(int courseId, int studentId)
    {
        var isEnrolled = _context.Enrollments.Where(en => en.CourseId == courseId && en.StudentId == studentId).Any();
        if (isEnrolled)
        {
            return true;
        }
        return false;
    }


    public async Task<bool> EnrollCourse(int courseId, int studentId)
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
}
