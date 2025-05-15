using DataAccessLayer.ViewModels;

namespace DataAccessLayer.Repository.Interface;

public interface ICourseRepository
{
    public IQueryable<CourseViewModel> GetCourseList();
    public Task<bool> AddCourse(CourseViewModel courseVM);
    public CourseViewModel SetUpdateModal(int id);
    public Task<bool> UpdateCourse(CourseViewModel courseVM);
    public Task<bool> DeleteCourse(int id);
    public IQueryable<CourseViewModel> GetCourseListStudent(int studentId);
    public bool IsAlreadyEnrolled(int courseId, int studentId);
    public bool isAnyEnrolled(int courseId);

    public bool IsAlreadyCompleted(int courseId, int studentId);
    public Task<bool> EnrollCourse(int courseId, int studentId);

    public Task<bool> WithdrawCourse(int courseId, int studentId);
    public Task<bool> CompleteCourse(int courseId, int studentId);

}
