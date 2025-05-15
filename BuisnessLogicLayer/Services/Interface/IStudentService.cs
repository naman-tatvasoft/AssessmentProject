using DataAccessLayer.ViewModels;

namespace BuisnessLogicLayer.Services.Interface;

public interface IStudentService
{
    public Task<bool> RegisterStudent(RegisterViewModel registerData);

}
