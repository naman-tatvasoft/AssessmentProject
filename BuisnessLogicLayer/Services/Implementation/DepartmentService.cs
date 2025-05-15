using BuisnessLogicLayer.Services.Interface;
using DataAccessLayer.Models;
using DataAccessLayer.Repository.Interface;

namespace BuisnessLogicLayer.Services.Implementation;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
    public IEnumerable<Department> GetDepartment()
    {
        return _departmentRepository.GetDepartment();
    }

}
