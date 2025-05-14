using BuisnessLogicLayer.Services.Interface;
using BuisnessLogicLayer.Helper;
using DataAccessLayer.ViewModels;
using DataAccessLayer.Repository.Interface;

namespace BuisnessLogicLayer.Services.Implementation;

public class UserCredService : IUserCredService
{
    private readonly IUserCredRepository _userCredRepository;
    private readonly JWTService _jwtService;
    public UserCredService(IUserCredRepository userCredRepository, JWTService jwtService)
    {
        this._userCredRepository = userCredRepository;
        this._jwtService = jwtService;
    }

    public string VerifyUser(UserCredViewModel userCred)
    {
        var data = _userCredRepository.VerifyUser(userCred.Email);
        if (data != null && data.Password == userCred.Password)
        {
            var role_obj = _userCredRepository.JwtRoleName(data);
            var token = _jwtService.GenerateToken(userCred.Email, role_obj);
            return token;
        } 
        return null;
    }
}
