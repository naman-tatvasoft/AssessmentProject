using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.ViewModels;
using BuisnessLogicLayer.Helper;
using BuisnessLogicLayer.Services.Interface;


namespace AssessmentProject.Controllers;

public class UserCredController : Controller
{
    private readonly IUserCredService _usercredService;
    private readonly int _tokenDuration;
    private readonly JWTService _jwtService;
    public UserCredController(IUserCredService usercredService, IConfiguration configuration, JWTService jwtService)
    {
        this._usercredService = usercredService;
        this._tokenDuration = configuration.GetValue<int>("JwtConfig:Duration");
        this._jwtService = jwtService;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        if (Request.Cookies.ContainsKey("AuthToken"))
        {
            string? token = Request.Cookies["AuthToken"];
            var claims = _jwtService.GetClaimsFromToken(token);
            if (claims != null)
            {
                string email = _jwtService.GetClaimValue(token, "email");
                string role = _jwtService.GetClaimValue(token, "role");
                ViewBag.Email = email;
                ViewBag.Role = role;
                return RedirectToAction("Index", "Dashboard");
            }
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult VerifyUser(UserCredViewModel userCred)
    {
        var verification = _usercredService.VerifyUser(userCred);
        CookieOptions option = new CookieOptions();

        if (verification != null)
        {
            if (userCred.RememberMe)
            {
                option.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Append("AuthToken", verification, option);
            }
            else
            {
                option.Expires = DateTime.Now.AddMinutes(_tokenDuration);
                Response.Cookies.Append("AuthToken", verification, option);
            }
            return RedirectToAction("Index", "Dashboard");
        }
        return View("Index");
    }

    public IActionResult Logout()
    {
        Response.Cookies.Delete("AuthToken");
        Response.Headers["Clear-Site-Data"] = "\"cache\", \"cookies\", \"storage\"";
        return RedirectToAction("Index", "UserCred");
    }


}
