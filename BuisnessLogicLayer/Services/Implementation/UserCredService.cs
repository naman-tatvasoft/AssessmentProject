using BuisnessLogicLayer.Services.Interface;
using BuisnessLogicLayer.Helper;
using DataAccessLayer.ViewModels;
using DataAccessLayer.Repository.Interface;
using System.Diagnostics.CodeAnalysis;

namespace BuisnessLogicLayer.Services.Implementation;

public class UserCredService : IUserCredService
{
    private readonly IUserCredRepository _userCredRepository;
    private readonly JWTHelper _jwtHelper;
    private readonly PasswordHashHelper _hashPassword;
    public UserCredService(IUserCredRepository userCredRepository, JWTHelper jwtHelper, PasswordHashHelper hashPassword)
    {
        this._userCredRepository = userCredRepository;
        this._jwtHelper = jwtHelper;
        this._hashPassword = hashPassword;
    }

    public string VerifyUser(UserCredViewModel userCred)
    {
        var data = _userCredRepository.GetUserData(userCred.Email);
        if (data != null && data.Password == _hashPassword.EncryptPassword(userCred.Password))
        {
            var role_obj = _userCredRepository.GetRoleName(data);
            var token = _jwtHelper.GenerateToken(userCred.Email, role_obj);
            return token;
        } 
        return null;
    }
    
    public int GetUserId(string email){
        return _userCredRepository.GetUserId(email);
    }
}
