using DataAccessLayer.ViewModels;

namespace BuisnessLogicLayer.Services.Interface;

public interface ICourseService
{
    public PaginationViewModel<CourseViewModel> GetCourseList(string search, string sortColumn, string sortDirection, int pageNumber, int pageSize);
    public Task<bool> AddCourse(CourseViewModel courseVM);
    public CourseViewModel SetUpdateModal(int id);
    public Task<bool> UpdateCourse(CourseViewModel projVM);
    public Task<bool> DeleteCourse(int id);
    public PaginationViewModel<CourseViewModel> GetCourseListStudent(int studentId, string search, string sortColumn, string sortDirection, int pageNumber, int pageSize);
    public bool IsAlreadyEnrolled(int courseId, int studentId);
    public bool isAnyEnrolled(int courseId);

    public bool IsAlreadyCompleted(int courseId, int studentId);
    public Task<bool> EnrollCourse(int courseId, int studentId);
    public Task<bool> WithdrawCourse(int courseId, int studentId);
    public Task<bool> CompleteCourse(int courseId, int studentId);


}
