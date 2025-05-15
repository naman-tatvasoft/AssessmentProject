using BuisnessLogicLayer.Services.Interface;
using DataAccessLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AssessmentProject.Controllers;

public class CourseController : Controller
{
    private readonly ICourseService _courseService;
    private readonly IDepartmentService _departmentService;
    public CourseController(ICourseService courseService, IDepartmentService departmentService)
    {
        this._courseService = courseService;
        this._departmentService = departmentService;
    }


    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult GetCourseList(string search, string sortColumn, string sortDirection, int pageNumber, int pageSize)
    {
        PaginationViewModel<CourseViewModel> courseVM = _courseService.GetCourseList(search, sortColumn, sortDirection, pageNumber, pageSize);
        return PartialView("_CourseListPartial", courseVM);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult SetAddModal()
    {
        var model = new CourseViewModel();
        ViewBag.DepartmentFields = new SelectList(_departmentService.GetDepartment(), "Id", "Name");
        return PartialView("_AddModalPartial", model);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddCourse([FromForm] CourseViewModel courseVM)
    {
        var isAdded = await _courseService.AddCourse(courseVM);
        if (isAdded)
        {

            return Json(new { success = true, message = "Course Added successfully" });
        }
        return Json(new { success = false, message = "Course Not Added!!!Please Try Again" });
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult SetUpdateModal(int id)
    {
        CourseViewModel model = _courseService.SetUpdateModal(id);
        ViewBag.DepartmentFields = new SelectList(_departmentService.GetDepartment(), "Id", "Name");
        return PartialView("_UpdateModalPartial", model);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCourse([FromForm] CourseViewModel courseVM)
    {
        var isUpdated = await _courseService.UpdateCourse(courseVM);
        if (isUpdated)
        {
            return Json(new { success = true, message = "Course Updated successfully" });
        }
        return Json(new { success = false, message = "Course Not Updated" });
    }

    // [HttpPost]
    // public async Task<IActionResult> DeleteCourse(int id){
    //     var isDeleted = await _courseService.DeleteCourse(id);
    //     if (isDeleted)
    //     {
    //         return Json(new { success = true, message = "Course Deleted successfully" });
    //     }
    //     return Json(new { success = false, message = "Course Not Deleted" });
    // }
}
