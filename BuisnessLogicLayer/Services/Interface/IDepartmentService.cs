using DataAccessLayer.Models;

namespace BuisnessLogicLayer.Services.Interface;

public interface IDepartmentService
{
    public IEnumerable<Department> GetDepartment();

}
