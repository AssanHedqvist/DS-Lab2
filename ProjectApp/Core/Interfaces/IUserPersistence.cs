using ProjectApp.Areas.Identity.Data;

namespace ProjectApp.Core.Interfaces;

public interface IUserPersistence
{
    List<AppIdentityUser> GetAllUsers();
}