using DataAccessLayer.ViewModels;

namespace BuisnessLogicLayer.Services.Interface;

public interface IUserCredService
{
    public string VerifyUser(UserCredViewModel userCred);

}
