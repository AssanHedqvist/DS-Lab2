using ProjectApp.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using ProjectApp.Core;

namespace ProjectApp.Models.Auctions;

public class UserVm
{
    [ScaffoldColumn(false)]
    public string Id { get; set; }

    [Display(Name = "User Name")]
    public string UserName { get; set; }

    [Display(Name = "Email")]
    public string Email { get; set; }

    public static UserVm FromAppIdentityUser(AppIdentityUser user)
    {
        return new UserVm()
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };
    }
    
    
}