using BuisnessLogicLayer.Services.Interface;
using BuisnessLogicLayer.Helper;
using DataAccessLayer.ViewModels;
using DataAccessLayer.Repository.Interface;

namespace BuisnessLogicLayer.Services.Implementation;

public class UserCredService : IUserCredService
{
    private readonly IUserCredRepository _userCredRepository;
    private readonly JWTHelper _jwtHelper;
    public UserCredService(IUserCredRepository userCredRepository, JWTHelper jwtHelper)
    {
        this._userCredRepository = userCredRepository;
        this._jwtHelper = jwtHelper;
    }

    public string VerifyUser(UserCredViewModel userCred)
    {
        var data = _userCredRepository.GetUserData(userCred.Email);
        if (data != null && data.Password == userCred.Password)
        {
            var role_obj = _userCredRepository.GetRoleName(data);
            var token = _jwtHelper.GenerateToken(userCred.Email, role_obj);
            return token;
        } 
        return null;
    }
}
