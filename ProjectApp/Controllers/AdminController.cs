using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectApp.Areas.Identity.Data;
using ProjectApp.Models.Auctions;

namespace ProjectApp.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<AppIdentityUser> _userManager;

    public AdminController(UserManager<AppIdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public ActionResult ListAllUsers()
    {
        var users = _userManager.Users.ToList();
        var userVms = users.Select(UserVm.FromAppIdentityUser).ToList();
        return View(userVms);
    }
    

}
    
    
