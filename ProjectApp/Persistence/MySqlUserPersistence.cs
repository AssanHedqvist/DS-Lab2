using ProjectApp.Areas.Identity.Data;
using ProjectApp.Core.Interfaces;

namespace ProjectApp.Persistence;

public class MySqlUserPersistence : IUserPersistence
{
    private readonly AppIdentityDbContext _dbContext;
    
    public MySqlUserPersistence(AppIdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public List<AppIdentityUser> GetAllUsers()
    {
        return null;
    }  
}