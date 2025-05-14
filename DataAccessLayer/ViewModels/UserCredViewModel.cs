using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.ViewModels;

public class UserCredViewModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}
