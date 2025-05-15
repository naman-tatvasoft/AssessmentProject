using DataAccessLayer.Models;
using DataAccessLayer.Repository.Interface;

namespace DataAccessLayer.Repository.Implementation;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly AssessmentProjectDbContext _context;

    public DepartmentRepository(AssessmentProjectDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Department> GetDepartment()
    {
        return _context.Departments;
    }

}
