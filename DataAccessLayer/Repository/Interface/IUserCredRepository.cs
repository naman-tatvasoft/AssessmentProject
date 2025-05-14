using DataAccessLayer.Models;

namespace DataAccessLayer.Repository.Interface;

public interface IUserCredRepository
{
    public string JwtRoleName(UserCred userCred);

    public UserCred VerifyUser(string email);

}
