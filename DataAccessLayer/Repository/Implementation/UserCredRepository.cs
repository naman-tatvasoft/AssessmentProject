using DataAccessLayer.Models;
using DataAccessLayer.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository.Implementation;

public class UserCredRepository : IUserCredRepository
{
    private readonly AssessmentProjectDbContext _context;

    public UserCredRepository(AssessmentProjectDbContext context)
    {
        _context = context;
    }

    public string GetRoleName(UserCred userCred)
    {
        try
        {
            var userObj = _context.Users.FirstOrDefault(e => e.Id == userCred.Id);
            var roleObj = _context.Roles.FirstOrDefault(e => e.Id == userObj.RoleId);
            return roleObj.Name;
        }
        catch
        {
            throw;
        }
    }
    public UserCred GetUserData(string email)
    {
        try
        {
            var data = _context.Users.FirstOrDefault(e => e.Email == email);
            return data;
        }
        catch
        {
            throw;
        }
    }

    public int GetUserId(string email)
    {
        return _context.Students.Include(s => s.UserCred).FirstOrDefault(s => s.UserCred.Email == email).Id;
    }

}
