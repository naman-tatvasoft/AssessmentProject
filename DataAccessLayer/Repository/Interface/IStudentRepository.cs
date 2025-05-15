using DataAccessLayer.Models;
using DataAccessLayer.ViewModels;

namespace DataAccessLayer.Repository.Interface;

public interface IStudentRepository
{
    public Task<Student> RegisterStudent(RegisterViewModel registerData);
    public List<StudentHistoryViewModel> GetStudentList(int courseId);
     public List<CourseViewModel> GetMyCourses(int studentId);
    public ProfileViewModel GetProfile(int studentId);

}
