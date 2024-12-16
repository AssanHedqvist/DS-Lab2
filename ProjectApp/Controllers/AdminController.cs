using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectApp.Areas.Identity.Data;
using ProjectApp.Core;
using ProjectApp.Core.Interfaces;
using ProjectApp.Models.Auctions;

namespace ProjectApp.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<AppIdentityUser> _userManager;
    private readonly IAuctionService _auctionService;

    public AdminController(UserManager<AppIdentityUser> userManager, IAuctionService auctionService)
    {
        _userManager = userManager;
        _auctionService = auctionService;
    }

    public ActionResult ListAllUsers()
    {
        var users = _userManager.Users.ToList();
        var userVms = users.Select(UserVm.FromAppIdentityUser).ToList();
        return View(userVms);
    }

    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        
        if (user != null)
        {
            var auctions = _auctionService.GetAuctionsByUser(user.UserName);
            foreach (var auction in auctions)
            {
                _auctionService.DeleteAuction(auction.Id, user.UserName);
            }
            await _userManager.DeleteAsync(user);
            
            
        }
        return RedirectToAction(nameof(ListAllUsers));
    }

    public ActionResult ViewUserAuctions(string id)
    {
        var user = _userManager.FindByIdAsync(id).Result;
        if (user == null)
        {
            return NotFound();
        }

        var auctions = _auctionService.GetAuctionsByUser(user.UserName);
        var auctionVms = auctions.Select(AuctionVm.FromAuction).ToList();
        return View(auctionVms);
    }

    public ActionResult DeleteAuction(int id, string username)
    {
        _auctionService.DeleteAuction(id, username);
        return RedirectToAction(nameof(ListAllUsers));
    }
}
    
    
