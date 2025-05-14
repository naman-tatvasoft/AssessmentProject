using DataAccessLayer.Models;
using DataAccessLayer.Repository.Interface;

namespace DataAccessLayer.Repository.Implementation;

public class UserCredRepository : IUserCredRepository
{
    private readonly AssessmentProjectDbContext _context;

    public UserCredRepository(AssessmentProjectDbContext context)
    {
        _context = context;
    }

    public string JwtRoleName(UserCred userCred)
    {
        var userObj = _context.Users.FirstOrDefault(e => e.Id == userCred.Id);
        var roleObj = _context.Roles.FirstOrDefault(e => e.Id == userObj.RoleId);
    
        return roleObj.Name;
    }
    public UserCred VerifyUser(string email)
    {
        var data = _context.Users.FirstOrDefault(e => e.Email == email);
        return data;
    }

}
