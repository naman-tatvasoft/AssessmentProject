using DataAccessLayer.Models;
using DataAccessLayer.Repository.Interface;
using DataAccessLayer.ViewModels;

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
            .Select(x => new CourseViewModel
            {
                Id = x.Id,
                CourseName = x.Name,
                Content = x.Content,
                Credits = x.Credits,
                Department = _context.Departments.FirstOrDefault(c => c.Id == x.DepartmentId).Name,
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
            DepartmentId = courseVM.DepartmentId
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
                DepartmentId = course.DepartmentId
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
           
            _context.Update(course);
        }

        return await _context.SaveChangesAsync() > 0;
    }

    // public async Task<bool> DeleteCourse(int id)
    // {
    //     var course = _context.Courses.FirstOrDefault(pr => pr.Id == id);
    //     if (course != null)
    //     {
    //         course.IsDeleted = true;
    //         _context.Update(course);
    //     }
    //     return await _context.SaveChangesAsync() > 0;
    // }
}
