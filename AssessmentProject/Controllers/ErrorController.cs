using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssessmentProject.Controllers;
[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]

public class ErrorController : Controller
{
    [Route("Error/Unauthorized")]
    public IActionResult Unauthorized()
    {
        return View("Unauthorized");
    }

    [Route("Error/Forbidden")]
    public IActionResult Forbidden()
    {
        return View("Forbidden");
    }

}