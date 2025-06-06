using BuisnessLogicLayer.Services.Interface;
using DataAccessLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AssessmentProject.Controllers;

public class StudentController : Controller
{
    private readonly IStudentService _studentService;
    private readonly IDepartmentService _departmentService;
    public StudentController(IStudentService studentService, IDepartmentService departmentService)
    {
        this._studentService = studentService;
        this._departmentService = departmentService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        ViewBag.DepartmentFields = new SelectList(_departmentService.GetDepartment(), "Id", "Name");
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> RegisterStudent(RegisterViewModel studentData)
    {
        var verification = await _studentService.RegisterStudent(studentData);

        if(verification){

            TempData["SuccessMessage"] = "Registerd Successfully";
            return RedirectToAction("Index", "UserCred");
        }
        TempData["ErrorMessage"] = "Registration Failed!! Please try again ";
        return RedirectToAction("Register", "Student");
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult SetStudentModal(int courseId)
    {
        List<StudentHistoryViewModel> studentList = _studentService.GetStudentList(courseId);
        return PartialView("_StudentHistoryPartial", studentList);
    }
    

    [HttpGet]
    [Authorize(Roles ="User")]
    public IActionResult MyCourse(){
        var studentId = int.Parse(Request.Cookies["UserId"]);
        List<CourseViewModel> list = _studentService.GetMyCourses(studentId);
        return View(list);
    }

    [HttpGet]
    [Authorize(Roles ="User")]
    public IActionResult Profile(){
        var studentId = int.Parse(Request.Cookies["UserId"]);
        ProfileViewModel prof = _studentService.GetProfile(studentId);
        return View(prof);
    }

}
