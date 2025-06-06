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
    private readonly JWTHelper _jwtHelper;
    public UserCredController(IUserCredService usercredService, IConfiguration configuration, JWTHelper jwtHelper)
    {
        this._usercredService = usercredService;
        this._tokenDuration = configuration.GetValue<int>("JwtConfig:Duration");
        this._jwtHelper = jwtHelper;
    }

    [AllowAnonymous]
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

    [HttpPost]
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

                var claims = _jwtHelper.GetClaimsFromToken(verification);
                if (claims != null)
                {
                    var role = _jwtHelper.GetClaimValue(verification, "role");
                    if (role == "User")
                    {
                        Response.Cookies.Append("UserId", _usercredService.GetUserId(userCred.Email).ToString(), option);
                    }
                }

            }
            else
            {
                option.Expires = DateTime.Now.AddMinutes(_tokenDuration);
                Response.Cookies.Append("AuthToken", verification, option);
                var claims = _jwtHelper.GetClaimsFromToken(verification);
                if (claims != null)
                {
                    var role = _jwtHelper.GetClaimValue(verification, "role");
                    if (role == "User")
                    {
                        Response.Cookies.Append("UserId", _usercredService.GetUserId(userCred.Email).ToString(), option);
                    }
                }
            }
            TempData["SuccessMessage"] = "Login Successfull";

            return RedirectToAction("Index", "Dashboard");
        }
        TempData["ErrorMessage"] = "Invalid Credentials";
        return RedirectToAction("Index", "UserCred");
    }

    public IActionResult Logout()
    {
        Response.Cookies.Delete("AuthToken");
        Response.Headers["Clear-Site-Data"] = "\"cache\", \"cookies\", \"storage\"";
        return RedirectToAction("Index", "UserCred");
    }




}
