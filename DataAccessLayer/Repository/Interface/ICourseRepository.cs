using DataAccessLayer.ViewModels;

namespace DataAccessLayer.Repository.Interface;

public interface ICourseRepository
{
    public IQueryable<CourseViewModel> GetCourseList();
    public Task<bool> AddCourse(CourseViewModel courseVM);
    public CourseViewModel SetUpdateModal(int id);
    public Task<bool> UpdateCourse(CourseViewModel courseVM);
    // public Task<bool> DeleteCourse(int id);

}
