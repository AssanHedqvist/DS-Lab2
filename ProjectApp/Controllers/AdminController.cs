using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectApp.Areas.Identity.Data;

namespace ProjectApp.Controllers;

public class AdminController : Controller
{
    private readonly UserManager<AppIdentityUser> _userManager;

    public AdminController(UserManager<AppIdentityUser> userManager)
    {
        _userManager = userManager;
    }
    

}