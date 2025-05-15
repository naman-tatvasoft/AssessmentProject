using DataAccessLayer.ViewModels;

namespace BuisnessLogicLayer.Services.Interface;

public interface IUserCredService
{
    public string VerifyUser(UserCredViewModel userCred);
    public int GetUserId(string email);

}
