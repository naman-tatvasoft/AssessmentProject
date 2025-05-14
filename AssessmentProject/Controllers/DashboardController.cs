using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AssessmentProject.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index(){
        return View();
    }
}
