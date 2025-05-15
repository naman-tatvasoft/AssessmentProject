using DataAccessLayer.ViewModels;

namespace BuisnessLogicLayer.Services.Interface;

public interface IStudentService
{
    public Task<bool> RegisterStudent(RegisterViewModel registerData);
    public List<StudentHistoryViewModel> GetStudentList(int courseId);
    public List<CourseViewModel> GetMyCourses(int studentId);
    public ProfileViewModel GetProfile(int studentId);
}
