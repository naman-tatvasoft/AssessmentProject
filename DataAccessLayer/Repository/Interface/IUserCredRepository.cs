using DataAccessLayer.Models;

namespace DataAccessLayer.Repository.Interface;

public interface IUserCredRepository
{
    public string GetRoleName(UserCred userCred);
    public UserCred GetUserData(string email);

}
