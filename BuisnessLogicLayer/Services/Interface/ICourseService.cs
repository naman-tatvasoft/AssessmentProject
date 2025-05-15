using DataAccessLayer.ViewModels;

namespace BuisnessLogicLayer.Services.Interface;

public interface ICourseService
{
        public PaginationViewModel<CourseViewModel> GetCourseList(string search, string sortColumn, string sortDirection, int pageNumber, int pageSize);
        public Task<bool> AddCourse(CourseViewModel courseVM);
        public CourseViewModel SetUpdateModal(int id);
        public Task<bool> UpdateCourse(CourseViewModel projVM);
        public Task<bool> DeleteCourse(int id);

}
