using BuisnessLogicLayer.Services.Interface;
using BuisnessLogicLayer.Helper;
using DataAccessLayer.Repository.Interface;
using DataAccessLayer.ViewModels;


namespace BuisnessLogicLayer.Services.Implementation;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly PasswordHashHelper _hashPassword;
    public StudentService(IStudentRepository studentRepository, PasswordHashHelper hashPassword)
    {
        this._studentRepository = studentRepository;
        this._hashPassword = hashPassword;
    }



    public async Task<bool> RegisterStudent(RegisterViewModel registerData)
    {
        registerData.Password = _hashPassword.EncryptPassword(registerData.Password);
        var data = await _studentRepository.RegisterStudent(registerData);
        if (data != null)
        {
            return true;
        }
        return false;
    }

    public List<StudentHistoryViewModel> GetStudentList(int courseId)
    {
        var studentData = _studentRepository.GetStudentList(courseId);
        return studentData;
    }

    public List<CourseViewModel> GetMyCourses(int studentId)
    {
        var studentCourseData = _studentRepository.GetMyCourses(studentId);
        return studentCourseData;
    }
    public ProfileViewModel GetProfile(int studentId){
         var studentData = _studentRepository.GetProfile(studentId);
        return studentData;
    }
    

}
