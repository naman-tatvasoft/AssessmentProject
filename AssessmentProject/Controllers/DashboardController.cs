using BuisnessLogicLayer.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AssessmentProject.Controllers;

public class DashboardController : Controller
{
    private readonly JWTHelper _jwtHelper;
    public DashboardController(JWTHelper jwtHelper)
    {
        this._jwtHelper = jwtHelper;
    }
    public IActionResult Index()
    {
        if (Request.Cookies.ContainsKey("AuthToken"))
        {
            string? token = Request.Cookies["AuthToken"];
            var claims = _jwtHelper.GetClaimsFromToken(token);
            if (claims != null)
            {
                var role = _jwtHelper.GetClaimValue(token, "role");
                if (role == "Admin")
                {
                    return RedirectToAction("AdminDashboard", "Dashboard");
                }
                else if (role == "User")
                {
                    return RedirectToAction("StudentDashboard", "Dashboard");
                }
            }
        }
        return View();
    }

    [Authorize(Roles = "Admin")]
    public IActionResult AdminDashboard()
    {
        return View();
    }

    [Authorize(Roles = "User")]
    public IActionResult StudentDashboard()
    {
        string Email = Request.Cookies["Email"];
        return View();
    }
}
