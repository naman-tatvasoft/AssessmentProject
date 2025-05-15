using BuisnessLogicLayer.Services.Interface;
using DataAccessLayer.Repository.Interface;
using DataAccessLayer.ViewModels;

namespace BuisnessLogicLayer.Services.Implementation;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public PaginationViewModel<CourseViewModel> GetCourseList(string search, string sortColumn, string sortDirection, int pageNumber, int pageSize)
    {

        var query = _courseRepository.GetCourseList();

        //search
        if (!string.IsNullOrEmpty(search))
        {
            string lowerSearchTerm = search.ToLower();
            query = query.Where(u =>
                u.CourseName.ToLower().Contains(lowerSearchTerm) || u.Department.ToLower().Contains(lowerSearchTerm)
            );
        }

        //sort
        if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortDirection))
        {
            if (sortColumn == "Id")
            {
                query = sortDirection == "asc" ? query.OrderBy(u => u.Id) : query.OrderByDescending(u => u.Id);
            }
            else if (sortColumn == "Name")
            {
                query = sortDirection == "asc" ? query.OrderBy(u => u.CourseName) : query.OrderByDescending(u => u.CourseName);
            }
            else if (sortColumn == "Department")
            {
                query = sortDirection == "asc" ? query.OrderBy(u => u.Department) : query.OrderByDescending(u => u.Department);
            }
        }

        int totalCount = query.Count();

        var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new PaginationViewModel<CourseViewModel>(items, totalCount, pageNumber, pageSize);
    }

    public async Task<bool> AddCourse(CourseViewModel courseVM)
    {
        return await _courseRepository.AddCourse(courseVM);
    }

     public CourseViewModel SetUpdateModal(int id)
    {
        return _courseRepository.SetUpdateModal(id);
    }

    public async Task<bool> UpdateCourse(CourseViewModel courseVM)
    {
        return await _courseRepository.UpdateCourse(courseVM);
    }

    public async Task<bool> DeleteCourse(int id)
    {
        return await _courseRepository.DeleteCourse(id);
    }

}
