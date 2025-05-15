using DataAccessLayer.Models;

namespace DataAccessLayer.Repository.Interface;

public interface IDepartmentRepository
{
    public IEnumerable<Department> GetDepartment();

}
